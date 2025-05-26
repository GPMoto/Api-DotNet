# Problema
A empresa Mottu enfrenta problemas pela falta de um sistema eficiente para a gestão de frotas nos pátios das suas filiais, o que compromete a automatização dos processos de identificação e controle das motocicletas. Como consequência, a verificação e localização dos veículos são realizadas manualmente, tornando o processo demorado e propenso a falhas.

# GPSMottu - Nova forma de gerir pátios
Nossa solução propõe a utilização integrada de visão computacional e tecnologia UWB (Ultra Wideband) para realizar a localização precisa e em tempo real de motocicletas nos pátios das filiais. Ao entrar no pátio, a moto é identificada automaticamente por câmeras equipadas com algoritmos de visão computacional, que reconhecem sua placa ou características visuais. Esse processo associa a moto a uma tag UWB acoplada, criando um vínculo entre a identidade da moto e sua futura localização no espaço físico do pátio.

A posição da moto é continuamente rastreada por meio de âncoras UWB distribuídas estrategicamente no pátio, que triangulam a posição das tags com uma precisão de aproximadamente 20 a 30 centímetros, mesmo em ambientes densos e sujeitos a interferências metálicas. Os dados são enviados a um servidor local que, por sua vez, atualiza em tempo real a posição de cada moto em um mapa interativo da planta do pátio, facilitando a gestão auditoria e operações internas de retirada, separação e logística.

Essa abordagem oferece uma infraestrutura escalável e com longa vida útil, além de um número enxuto de âncoras para cobrir os pátios. A solução é facilmente replicável em outras unidades da empresa e proporciona ganhos expressivos em agilidade, controle operacional e eficiência no gerenciamento do pátio.

# Equipe


- Gustavo Dias da Silva Cruz - RM556448

- Júlia Medeiros Angelozi - RM556364

- Felipe Ribeiro Tardochi da Silva - RM555100


# Rotas
## Cidade
### Get
- Get all: `https://localhost:7095/cidades`
- GetbyId:`https://localhost:7095/cidades/{id}`
### Post
- Post: `https://localhost:7095/cidades`
### Put
- Put: `https://localhost:7095/cidades/{id}`
### Delete
- Delete: `https://localhost:7095/cidades/{id}`


- ## Contato
### Get
- Get all: `https://localhost:7095/contatos`
- GetbyId:`https://localhost:7095/contatos/{id}`
- GetByNomeDono : `https://localhost:7095/contatos/nomeDono/{nome}`
### Post
- Post: `https://localhost:7095/contatos`
### Put
- Put: `https://localhost:7095/contatos/{id}`
### Delete
- Delete: `https://localhost:7095/contatos/{id}`


## Endereco
### Get
- Get all: `https://localhost:7095/enderecos`
- GetbyId:`https://localhost:7095/enderecos/{id}`
- GetbyCep: `https://localhost:7095/enderecos/cep/{cep}`
### Post
- Post: `https://localhost:7095/enderecos`
### Put
- Put: `https://localhost:7095/enderecos/{id}`
### Delete
- Delete: `https://localhost:7095/enderecos/{id}`

## Estado
### Get
- Get all: `https://localhost:7095/estados`
- GetbyId:`https://localhost:7095/estados/{id}`
### Post
- Post: `https://localhost:7095/estados`
### Put
- Put: `https://localhost:7095/estados/{id}`
### Delete
- Delete: `https://localhost:7095/estados/{id}`

## Filial
### Get
- Get all: `https://localhost:7095/filiais`
- GetbyId:`https://localhost:7095/filiais/{id}`
- GetbyCnpj: `https://localhost:7095/filiais/cnph/{cnpj}`
### Post
- Post: `https://localhost:7095/filiais`
### Put
- Put: `https://localhost:7095/filiais/{id}`
### Delete
- Delete: `https://localhost:7095/filiais/{id}`

## Moto
### Get
- Get all: `https://localhost:7095/motos`
- GetbyId:`https://localhost:7095/motos/{id}`
- GetByIdentificador : `https://localhost:7095/motos/identificador/{identificador}`
	- Identificador (placa,chassi, número do motor)
- GetByIdFilial: `https://localhost:7095/motos/filial/{id_filial}`
### Post
- Post: `https://localhost:7095/motos`
### Put
- Put: `https://localhost:7095/motos/{id}`
### Delete
- Delete: `https://localhost:7095/motos/{id}`

## Pais
### Get
- Get all: `https://localhost:7095/paises`
- GetbyId:`https://localhost:7095/paises/{id}`
### Post
- Post: `https://localhost:7095/paises`
### Put
- Put: `https://localhost:7095/paises/{id}`
### Delete
- Delete: `https://localhost:7095/paises/{id}`

## Perfil
### Get
- Get all: `https://localhost:7095/perfis`
- GetbyId:`https://localhost:7095/perfis/{id}`
### Post
- Post: `https://localhost:7095/perfis`
### Put
- Put: `https://localhost:7095/perfis/{id}`
### Delete
- Delete: `https://localhost:7095/perfis/{id}`

## SecoesFilial
### Get
- Get all: `https://localhost:7095/secoesfilial`
- GetbyId:`https://localhost:7095/secoesfilial/{id}`
- GetByIdFilial: `https://localhost:7095/secoesfilial/filial/{id_filial}`
### Post
- Post: `https://localhost:7095/secoesfilial`
### Put
- Put: `https://localhost:7095/secoesfilial/{id}`
### Delete
- Delete: `https://localhost:7095/secoesfilial/{id}`

## Telefone
### Get
- Get all: `https://localhost:7095/telefones`
- GetbyId:`https://localhost:7095/telefones/{id}`
### Post
- Post: `https://localhost:7095/telefones`
### Put
- Put: `https://localhost:7095/telefones/{id}`
### Delete
- Delete: `https://localhost:7095/telefones/{id}`

## TipoMoto
### Get
- Get all: `https://localhost:7095/tipomotos`
- GetbyId:`https://localhost:7095/tipomotos/{id}`
### Post
- Post: `https://localhost:7095/tipomotos`
### Put
- Put: `https://localhost:7095/tipomotos/{id}`
### Delete
- Delete: `https://localhost:7095/tipomotos/{id}`

## TipoSecaoFilial
### Get
- Get all: `https://localhost:7095/tiposecaofilial`
- GetbyId:`https://localhost:7095/tiposecaofilial/{id}`
### Post
- Post: `https://localhost:7095/tiposecaofilial`
### Put
- Put: `https://localhost:7095/tiposecaofilial/{id}`
### Delete
- Delete: `https://localhost:7095/tiposecaofilial/{id}`

## Usuario
### Get
- Get all: `https://localhost:7095/usuarios`
- GetbyId:`https://localhost:7095/usuarios/{id}`
- GetByIdFilial: `https://localhost:7095/usuarios/filial/{id_filial}`
### Post
- Post: `https://localhost:7095/usuarios`
### Put
- Put: `https://localhost:7095/usuarios/{id}`
### Delete
- Delete: `https://localhost:7095/usuarios/{id}`

## Uwb
### Get
- Get all: `https://localhost:7095/uwb`
- GetbyId:`https://localhost:7095/uwb/{id}`
### Post
- Post: `https://localhost:7095/uwb`
### Put
- Put: `https://localhost:7095/uwb/{id}`
### Delete
- Delete: `https://localhost:7095/uwb/{id}`


# Como Rodar o projeto localmente
## Pré-Requisitos
- .Net 9 sdk
- Link para o download : `https://dotnet.microsoft.com/pt-br/download/dotnet/9.0`

## 1. Clone o repositório
- `https://github.com/GPMoto/Api-DotNet.git`
- cd api-DotNet

## 2. Abra o arquivo .sln
- Abra o arquivo `ApiGpsMottu.sln`

## 3. Restaure os pacotes NuGet
- Abra o package manager console de: `cd .\WebApplication3`
- Acesse o terminal NuGet e rode o comando: `dotnet restore`

## 4. atualize o appsettings.json
- copie o seguinte conteudo e insira as suas informações:
<pre>
{

  "ConnectionStrings": {
    "DefaultConnection": "User Id=SeuID;Password=SuaSenha;Data Source=//SuaURL:1521/ORCL"

  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}

</pre>
### Atenção!!!
- É importante realizar esse passo para que o projeto funcione!!

## 5. Aplique as migrations no seu banco
- Rode o comando: Update-Database

## 6. Rode o projeto e acesse a url
- Utilize o comando: `dotnet run --launch-profile https`
- rode o projeto e acesse a url : `https://localhost:7095/swagger/index.html`
