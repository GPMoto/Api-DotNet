using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using WebApplication3.Data;
using WebApplication3.Models;
using WebApplication3.Repositories;
using WebApplication3.Service;

namespace WebApplication3.Tests;

/// <summary>
/// Testes para o fluxo principal do sistema GPS Mottu
/// Cobrindo: Autenticação, Usuários, Filiais, Motos e Hierarquia Geográfica
/// </summary>
public class GpsMottuMainFlowTests : IDisposable
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    // Repositories
    private readonly UsuarioRepository _usuarioRepository;
    private readonly FilialRepository _filialRepository;
    private readonly CidadeRepository _cidadeRepository;
    private readonly EstadoRepository _estadoRepository;
    private readonly PaisRepository _paisRepository;

    // Services
    private readonly UsuarioService _usuarioService;
    private readonly FilialService _filialService;
    private readonly TokenService _tokenService;

    public GpsMottuMainFlowTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(options);

        var configMock = new Mock<IConfiguration>();
        configMock.Setup(x => x["Jwt:Key"]).Returns("esta_e_uma_chave_secreta_super_grande_e_segura_com_numeros_12345");
        configMock.Setup(x => x["Jwt:Issuer"]).Returns("https://meusitegit.com");
        configMock.Setup(x => x["Jwt:Audience"]).Returns("https://audiencia.com");
        _configuration = configMock.Object;

        _usuarioRepository = new UsuarioRepository(_context);
        _filialRepository = new FilialRepository(_context);
        _cidadeRepository = new CidadeRepository(_context);
        _estadoRepository = new EstadoRepository(_context);
        _paisRepository = new PaisRepository(_context);

        _usuarioService = new UsuarioService(_usuarioRepository);
        _filialService = new FilialService(_filialRepository);
        _tokenService = new TokenService(_configuration);
    }

    #region Fluxo Principal 1: Hierarquia Geográfica (País → Estado → Cidade)

    [Fact]
    public async Task FluxoPrincipal_CriarHierarquiaGeografica_DeveSerSucessoECompleto()
    {
        var pais = new Pais { Id_pais = 1, NomePais = "Brasil" };
        var paisCriado = await _paisRepository.AddAsync(pais);

        var estado = new Estado { id_estado = 1, NomeEstado = "São Paulo", id_pais = paisCriado.Id_pais };
        var estadoCriado = await _estadoRepository.AddAsync(estado);

        var cidade = new Cidade { id_cidade = 1, NomeCidade = "São Paulo", id_estado = estadoCriado.id_estado };
        var cidadeCriada = await _cidadeRepository.AddAsync(cidade);

        paisCriado.Should().NotBeNull();
        paisCriado.NomePais.Should().Be("Brasil");

        estadoCriado.Should().NotBeNull();
        estadoCriado.NomeEstado.Should().Be("São Paulo");
        estadoCriado.id_pais.Should().Be(paisCriado.Id_pais);

        cidadeCriada.Should().NotBeNull();
        cidadeCriada.NomeCidade.Should().Be("São Paulo");
        cidadeCriada.id_estado.Should().Be(estadoCriado.id_estado);

        var paisNoBanco = await _paisRepository.GetByIdAsync(1);
        var estadoNoBanco = await _estadoRepository.GetByIdAsync(1);
        var cidadeNoBanco = await _cidadeRepository.GetByIdAsync(1);

        paisNoBanco.Should().NotBeNull();
        estadoNoBanco.Should().NotBeNull();
        cidadeNoBanco.Should().NotBeNull();
    }

    #endregion

    #region Fluxo Principal 2: Autenticação e Gestão de Usuários

    [Fact]
    public async Task FluxoPrincipal_CriarUsuarioEAutenticar_DeveGerarTokenValido()
    {
        await CriarDadosBase();

        var usuario = new Usuario
        {
            NomeUsuario = "Admin Sistema",
            EmailUsuario = "admin@gpmottu.com",
            SenhaUsuario = "senha123",
            id_perfil = 1,
            id_filial = 1
        };

        var usuarioCriado = await _usuarioService.AddAsync(usuario);

        var usuarioEncontrado = await _usuarioService.getByEmail("admin@gpmottu.com");

        var token = _tokenService.GenerateToken(usuarioEncontrado.EmailUsuario);

        usuarioCriado.Should().NotBeNull();
        usuarioCriado.EmailUsuario.Should().Be("admin@gpmottu.com");

        usuarioEncontrado.Should().NotBeNull();
        usuarioEncontrado.NomeUsuario.Should().Be("Admin Sistema");

        token.Should().NotBeNullOrEmpty();
        token.Split('.').Should().HaveCount(3);
    }

    [Fact]
    public async Task FluxoPrincipal_BuscarUsuarioInexistente_DeveLancarException()
    {
        var emailInexistente = "naoexiste@gpmottu.com";

        var exception = await Assert.ThrowsAsync<Exception>(
            () => _usuarioService.getByEmail(emailInexistente)
        );

        exception.Message.Should().Be("Usuário não encontrado");
    }

    [Fact]
    public async Task FluxoPrincipal_GerenciarUsuariosPorFilial_DeveRetornarUsuariosCorretos()
    {
        await CriarDadosBase();

        var usuarios = new List<Usuario>
        {
            new Usuario { NomeUsuario = "João Filial 1", EmailUsuario = "joao@f1.com", SenhaUsuario = "123", id_perfil = 1, id_filial = 1 },
            new Usuario { NomeUsuario = "Maria Filial 1", EmailUsuario = "maria@f1.com", SenhaUsuario = "123", id_perfil = 2, id_filial = 1 },
            new Usuario { NomeUsuario = "Pedro Filial 2", EmailUsuario = "pedro@f2.com", SenhaUsuario = "123", id_perfil = 1, id_filial = 2 }
        };

        foreach (var user in usuarios)
        {
            await _usuarioService.AddAsync(user);
        }

        var usuariosFilial1 = await _usuarioService.getByIdFilial(1);
        var usuariosFilial2 = await _usuarioService.getByIdFilial(2);
        var todosUsuarios = await _usuarioService.GetAllAsync();

        usuariosFilial1.Should().HaveCount(2);
        usuariosFilial1.Should().OnlyContain(u => u.id_filial == 1);

        usuariosFilial2.Should().HaveCount(1);
        usuariosFilial2.Should().OnlyContain(u => u.id_filial == 2);

        todosUsuarios.Should().HaveCount(3);
    }

    #endregion

    #region Fluxo Principal 3: Gestão de Filiais

    [Fact]
    public async Task FluxoPrincipal_CriarEGerenciarFilial_DeveSerCompleto()
    {
        await CriarDadosCompletos();

        var filial = new Filial
        {
            Cnpj = "12.345.678/0001-90",
            senha = "senhafilial123",
            id_endereco = 1,
            id_contato = 1
        };

        var filialCriada = await _filialService.AddAsync(filial);
        var filialEncontrada = await _filialService.GetByIdAsync(filialCriada.id_filial);
        var filiaisPorCNPJ = await _filialService.getByCNPJ("12.345.678/0001-90");

        filialCriada.senha = "novaSenha456";
        var filialAtualizada = await _filialService.UpdateAsync(filialCriada);

        filialCriada.Should().NotBeNull();
        filialCriada.Cnpj.Should().Be("12.345.678/0001-90");

        filialEncontrada.Should().NotBeNull();
        filialEncontrada!.Cnpj.Should().Be("12.345.678/0001-90");

        filiaisPorCNPJ.Should().HaveCount(1);
        filiaisPorCNPJ.First().Cnpj.Should().Be("12.345.678/0001-90");

        filialAtualizada.Should().NotBeNull();
        filialAtualizada.senha.Should().Be("novaSenha456");
    }

    [Fact]
    public async Task FluxoPrincipal_DeletarFilial_DeveRemoverCorretamente()
    {
        await CriarDadosCompletos();

        var filial = new Filial
        {
            Cnpj = "98.765.432/0001-10",
            senha = "senha123",
            id_endereco = 1,
            id_contato = 1
        };

        var filialCriada = await _filialService.AddAsync(filial);

        var resultado = await _filialService.DeleteAsync(filialCriada.id_filial);
        var filialDeletada = await _filialService.GetByIdAsync(filialCriada.id_filial);

        resultado.Should().BeTrue();
        filialDeletada.Should().BeNull();
    }

    #endregion

    #region Fluxo Principal 4: Integração Completa do Sistema

    [Fact]
    public async Task FluxoPrincipal_IntegracaoCompleta_SistemaGPSMottu()
    {
        var pais = await _paisRepository.AddAsync(new Pais { NomePais = "Brasil" });
        var estado = await _estadoRepository.AddAsync(new Estado { NomeEstado = "SP", id_pais = pais.Id_pais });
        var cidade = await _cidadeRepository.AddAsync(new Cidade { NomeCidade = "São Paulo", id_estado = estado.id_estado });

        var endereco = new Endereco
        {
            NomeLogradouro = "Av. Paulista",
            NumeroLogradouro = "1000",
            Cep = "01310-100",
            id_cidade = cidade.id_cidade
        };
        await _context.Endereco.AddAsync(endereco);

        var telefone = new Telefone { Ddi = "55", Ddd = "11", Numero = "999999999" };
        await _context.Telefone.AddAsync(telefone);

        var contato = new Contato
        {
            nmDono = "Responsável Filial",
            status = 1,
            id_Telefone = telefone.id_telefone
        };
        await _context.Contato.AddAsync(contato);

        var perfil = new Perfil { NomePerfil = "Administrador" };
        await _context.Perfil.AddAsync(perfil);

        await _context.SaveChangesAsync();

        // 3. Criar filial
        var filial = await _filialService.AddAsync(new Filial
        {
            Cnpj = "11.222.333/0001-44",
            senha = "filial123",
            id_endereco = endereco.id_endereco,
            id_contato = contato.id_contato
        });

        var admin = await _usuarioService.AddAsync(new Usuario
        {
            NomeUsuario = "Administrador Master",
            EmailUsuario = "admin@gpmottu.com.br",
            SenhaUsuario = "admin123",
            id_perfil = perfil.id_perfil,
            id_filial = filial.id_filial
        });

        var usuarioAutenticado = await _usuarioService.getByEmail("admin@gpmottu.com.br");
        var token = _tokenService.GenerateToken(usuarioAutenticado.EmailUsuario);

        var tipoMoto = new TipoMoto { NomeTipoMoto = "Esportiva" };
        await _context.TipoMoto.AddAsync(tipoMoto);

        var tipoSecao = new TipoSecao { NomeTipoSecao = "Estacionamento" };
        await _context.TipoSecao.AddAsync(tipoSecao);

        await _context.SaveChangesAsync();

        var secaoFilial = new SecoesFilial
        {
            Lado1 = 100,
            Lado2 = 200,
            Lado3 = 100,
            Lado4 = 200,
            id_tipo_secao = tipoSecao.id_tipo_secao,
            id_filial = filial.id_filial
        };
        await _context.SecoesFilial.AddAsync(secaoFilial);

        var moto = new Moto
        {
            Status = 1,
            CondicaoMoto = "Nova",
            IdentificadorMoto = "ABC-1234",
            id_filial = filial.id_filial,
            id_tipo_moto = tipoMoto.id_tipo_moto
        };
        await _context.Moto.AddAsync(moto);

        await _context.SaveChangesAsync();

        pais.Should().NotBeNull();
        estado.Should().NotBeNull();
        cidade.Should().NotBeNull();

        filial.Should().NotBeNull();
        filial.Cnpj.Should().Be("11.222.333/0001-44");

        admin.Should().NotBeNull();
        admin.EmailUsuario.Should().Be("admin@gpmottu.com.br");
        usuarioAutenticado.Should().NotBeNull();
        token.Should().NotBeNullOrEmpty();

        secaoFilial.Should().NotBeNull();
        moto.Should().NotBeNull();
        moto.IdentificadorMoto.Should().Be("ABC-1234");

        admin.id_filial.Should().Be(filial.id_filial);
        moto.id_filial.Should().Be(filial.id_filial);
        secaoFilial.id_filial.Should().Be(filial.id_filial);
    }

    #endregion

    #region Métodos Auxiliares

    private async Task CriarDadosBase()
    {
        var perfil = new Perfil { id_perfil = 1, NomePerfil = "Admin" };
        var perfil2 = new Perfil { id_perfil = 2, NomePerfil = "User" };
        await _context.Perfil.AddRangeAsync(perfil, perfil2);

        var pais = new Pais { Id_pais = 1, NomePais = "Brasil" };
        await _context.Pais.AddAsync(pais);

        var estado = new Estado { id_estado = 1, NomeEstado = "SP", id_pais = 1 };
        await _context.Estado.AddAsync(estado);

        var cidade = new Cidade { id_cidade = 1, NomeCidade = "São Paulo", id_estado = 1 };
        await _context.Cidade.AddAsync(cidade);

        var endereco = new Endereco { id_endereco = 1, NomeLogradouro = "Rua A", NumeroLogradouro = "123", Cep = "12345-678", id_cidade = 1 };
        await _context.Endereco.AddAsync(endereco);

        var telefone = new Telefone { id_telefone = 1, Ddi = "55", Ddd = "11", Numero = "999999999" };
        await _context.Telefone.AddAsync(telefone);

        var contato = new Contato { id_contato = 1, nmDono = "Contato Teste", status = 1, id_Telefone = 1 };
        await _context.Contato.AddAsync(contato);

        var filial1 = new Filial { id_filial = 1, Cnpj = "11.111.111/0001-11", senha = "123", id_endereco = 1, id_contato = 1 };
        var filial2 = new Filial { id_filial = 2, Cnpj = "22.222.222/0001-22", senha = "456", id_endereco = 1, id_contato = 1 };
        await _context.Filial.AddRangeAsync(filial1, filial2);

        await _context.SaveChangesAsync();
    }

    private async Task CriarDadosCompletos()
    {
        await CriarDadosBase();
    }

    #endregion

    public void Dispose()
    {
        _context.Dispose();
    }
}