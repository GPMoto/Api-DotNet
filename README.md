# GPSMottu - Nova forma de gerir p�tios
- Nossa solu��o consiste no uso de sensores de geolocaliza��o indoor (uwb) para a localiza��o precisa de motos no p�tio, assim diminuindo a perda de motos dentro do p�tio e aumentando a efici�ncia na localiza��o das motos.

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
	- Identificador (placa,chassi, n�mero do motor)
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
## Pr�-Requisitos
- .Net 9 sdk
- Link para o download : `https://dotnet.microsoft.com/pt-br/download/dotnet/9.0`

## 1. Clone o reposit�rio
- `https://github.com/GPMoto/Api-DotNet.git`
- cd api-DotNet

## 2. Restaure os pacotes NuGet
- Acesse o terminal NuGet e rode o comando: `dotnet restore`

## 3. Crie o appsetings.json
- copie o seguinte conteudo e insira as suas informa��es:
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
### Aten��o!!!
- � importante realizar esse passo para que o projeto funcione!!

## 4. Aplique as migrations no seu banco
- Rode o comando: Update-Database

## 5. Rode o projeto e acesse a url
- rode o projeto e acesse a url : `https://localhost:7095/swagger/index.html`
