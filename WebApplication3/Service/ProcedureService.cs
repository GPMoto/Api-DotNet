using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Microsoft.Extensions.Configuration;

namespace WebApplication3.Service
{
    public class OracleFunctionsService
    {
        private readonly string _connectionString;

        public OracleFunctionsService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Chamada simples da função printa_json
        public string PrintaJson(string chave, string valor)
        {
            OracleConnection connection = new OracleConnection(_connectionString);
            string resultado = "";

            try
            {
                connection.Open();

                // Chamada da função: SELECT pkg_functions.printa_json(?, ?) FROM dual
                OracleCommand cmd = new OracleCommand("select pkg_functions.printa_json(:chave, :valor) from dual", connection);
                cmd.CommandType = CommandType.Text;

                // Parâmetros de entrada
                OracleParameter paramChave = new OracleParameter("chave", OracleDbType.Varchar2);
                paramChave.Value = chave;
                cmd.Parameters.Add(paramChave);

                OracleParameter paramValor = new OracleParameter("valor", OracleDbType.Varchar2);
                paramValor.Value = valor;
                cmd.Parameters.Add(paramValor);

                // Executa e recupera o resultado
                OracleDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    resultado = reader.GetString(0);
                }
                reader.Close();
                cmd.Dispose();

                connection.Close();
            }
            catch (Exception e)
            {
                resultado = $"{{\"erro\": \"Falha ao executar função: {e.Message.Replace("\"", "'")}\"}}";
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
            }

            return resultado;
        }

        // Chamada usando CallableStatement (mais formal)
        public string PrintaJsonComCallable(string chave, string valor)
        {
            OracleConnection connection = new OracleConnection(_connectionString);
            string resultado = "";

            try
            {
                connection.Open();

                // Sintaxe com BEGIN/END para função
                OracleCommand cmd = new OracleCommand("begin ? := pkg_functions.printa_json(:chave, :valor); end;", connection);
                cmd.CommandType = CommandType.Text;

                // Parâmetro de retorno (posição 1)
                OracleParameter paramRetorno = new OracleParameter("retorno", OracleDbType.Varchar2, 32767)
                {
                    Direction = ParameterDirection.ReturnValue
                };
                cmd.Parameters.Add(paramRetorno);

                // Parâmetros de entrada
                OracleParameter paramChave = new OracleParameter("chave", OracleDbType.Varchar2);
                paramChave.Value = chave;
                cmd.Parameters.Add(paramChave);

                OracleParameter paramValor = new OracleParameter("valor", OracleDbType.Varchar2);
                paramValor.Value = valor;
                cmd.Parameters.Add(paramValor);

                cmd.ExecuteNonQuery();

                // Recupera o resultado
                resultado = paramRetorno.Value != DBNull.Value ? paramRetorno.Value.ToString() : "";

                cmd.Dispose();
                connection.Close();
            }
            catch (Exception e)
            {
                resultado = $"{{\"erro\": \"Falha ao executar função: {e.Message.Replace("\"", "'")}\"}}";
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
            }

            return resultado;
        }

        // Validar email
        public int ValidaEmailUser(string email)
        {
            OracleConnection connection = new OracleConnection(_connectionString);
            int resultado = -1;

            try
            {
                connection.Open();

                OracleCommand cmd = new OracleCommand("select pkg_functions.valida_email_user(:email) from dual", connection);
                cmd.CommandType = CommandType.Text;

                OracleParameter paramEmail = new OracleParameter("email", OracleDbType.Varchar2);
                paramEmail.Value = email;
                cmd.Parameters.Add(paramEmail);

                OracleDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    resultado = reader.GetInt32(0);
                }
                reader.Close();
                cmd.Dispose();

                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao validar email: {e.Message}");
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
            }

            return resultado;
        }

        // Buscar usuário completo do Oracle
        public Dictionary<string, object> GetUsuarioCompleto(int idUsuario)
        {
            OracleConnection connection = new OracleConnection(_connectionString);
            var dadosUsuario = new Dictionary<string, object>();

            try
            {
                connection.Open();

                string sql = @"
                    SELECT 
                        u.id_usuario,
                        u.nome_usuario,
                        u.email_usuario,
                        u.id_perfil,
                        u.id_filial,
                        p.nome_perfil,
                        f.cnpj,
                        e.nome_logradouro,
                        e.cep,
                        c.nome_cidade,
                        est.nome_estado
                    FROM usuario u
                    LEFT JOIN perfil p ON u.id_perfil = p.id_perfil
                    LEFT JOIN filial f ON u.id_filial = f.id_filial
                    LEFT JOIN endereco e ON f.id_endereco = e.id_endereco
                    LEFT JOIN cidade c ON e.id_cidade = c.id_cidade
                    LEFT JOIN estado est ON c.id_estado = est.id_estado
                    WHERE u.id_usuario = :id_usuario";

                OracleCommand cmd = new OracleCommand(sql, connection);
                cmd.Parameters.Add(new OracleParameter("id_usuario", idUsuario));

                OracleDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dadosUsuario["id_usuario"] = reader["id_usuario"];
                    dadosUsuario["nome_usuario"] = reader["nome_usuario"];
                    dadosUsuario["email_usuario"] = reader["email_usuario"];
                    dadosUsuario["id_perfil"] = reader["id_perfil"];
                    dadosUsuario["id_filial"] = reader["id_filial"];
                    dadosUsuario["nome_perfil"] = reader["nome_perfil"];
                    dadosUsuario["cnpj_filial"] = reader["cnpj"];
                    dadosUsuario["endereco"] = reader["nome_logradouro"];
                    dadosUsuario["cep"] = reader["cep"];
                    dadosUsuario["cidade"] = reader["nome_cidade"];
                    dadosUsuario["estado"] = reader["nome_estado"];
                }

                reader.Close();
                cmd.Dispose();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao buscar usuário completo: {e.Message}");
                dadosUsuario["erro"] = $"Erro ao buscar dados: {e.Message}";
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
            }

            return dadosUsuario;
        }

        // Buscar estatísticas do usuário
        public Dictionary<string, object> GetEstatisticasUsuario(int idUsuario)
        {
            OracleConnection connection = new OracleConnection(_connectionString);
            var estatisticas = new Dictionary<string, object>();

            try
            {
                connection.Open();

                string sql = @"
                    SELECT 
                        COUNT(*) as total_motos,
                        SUM(CASE WHEN m.status = 1 THEN 1 ELSE 0 END) as motos_ativas,
                        SUM(CASE WHEN m.status = 0 THEN 1 ELSE 0 END) as motos_inativas,
                        (SELECT COUNT(*) FROM usuario WHERE id_filial = u.id_filial) as total_usuarios_filial
                    FROM usuario u
                    LEFT JOIN moto m ON u.id_filial = m.id_filial
                    WHERE u.id_usuario = :id_usuario
                    GROUP BY u.id_filial";

                OracleCommand cmd = new OracleCommand(sql, connection);
                cmd.Parameters.Add(new OracleParameter("id_usuario", idUsuario));

                OracleDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    estatisticas["total_motos"] = reader["total_motos"];
                    estatisticas["motos_ativas"] = reader["motos_ativas"];
                    estatisticas["motos_inativas"] = reader["motos_inativas"];
                    estatisticas["total_usuarios_filial"] = reader["total_usuarios_filial"];
                }

                reader.Close();
                cmd.Dispose();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao buscar estatísticas: {e.Message}");
                estatisticas["erro"] = $"Erro ao buscar estatísticas: {e.Message}";
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
            }

            return estatisticas;
        }

        // Gerar JSON complexo com múltiplos dados
        public string GeraJsonCompleto(Dictionary<string, object> dados)
        {
            try
            {
                var jsonString = System.Text.Json.JsonSerializer.Serialize(dados, new System.Text.Json.JsonSerializerOptions
                {
                    WriteIndented = true
                });
                return jsonString;
            }
            catch (Exception e)
            {
                return $"{{\"erro\": \"Falha ao gerar JSON: {e.Message.Replace("\"", "'")}\"}}";
            }
        }
    }
}