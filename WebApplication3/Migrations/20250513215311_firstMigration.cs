using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication3.Migrations
{
    /// <inheritdoc />
    public partial class firstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_gpsMottu_contato",
                columns: table => new
                {
                    id_contato = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nm_dono = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    status = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    id_Telefone = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_gpsMottu_contato", x => x.id_contato);
                    table.ForeignKey(
                        name: "Contato_telefone_fk",
                        column: x => x.id_Telefone,
                        principalTable: "t_gpsMottu_contato",
                        principalColumn: "id_contato",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_gpsMottu_pais",
                columns: table => new
                {
                    Id_pais = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nm_pais = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_gpsMottu_pais", x => x.Id_pais);
                });

            migrationBuilder.CreateTable(
                name: "t_gpsMottu_perfil",
                columns: table => new
                {
                    id_perfil = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nm_perfil = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_gpsMottu_perfil", x => x.id_perfil);
                });

            migrationBuilder.CreateTable(
                name: "t_gpsMottu_telefone",
                columns: table => new
                {
                    id_telefone = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nr_ddd = table.Column<string>(type: "NVARCHAR2(3)", maxLength: 3, nullable: false),
                    nr_ddi = table.Column<string>(type: "NVARCHAR2(3)", maxLength: 3, nullable: false),
                    nr_telefone = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_gpsMottu_telefone", x => x.id_telefone);
                });

            migrationBuilder.CreateTable(
                name: "t_gpsMottu_tipo_moto",
                columns: table => new
                {
                    id_tipo_moto = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nm_tipo_moto = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_gpsMottu_tipo_moto", x => x.id_tipo_moto);
                });

            migrationBuilder.CreateTable(
                name: "t_gpsMottu_tipo_secao",
                columns: table => new
                {
                    id_tipo_secao = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nm_tipo_secao = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_gpsMottu_tipo_secao", x => x.id_tipo_secao);
                });

            migrationBuilder.CreateTable(
                name: "t_gpsMottu_estado",
                columns: table => new
                {
                    id_estado = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nm_estado = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    id_pais = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_gpsMottu_estado", x => x.id_estado);
                    table.ForeignKey(
                        name: "estado_pais",
                        column: x => x.id_pais,
                        principalTable: "t_gpsMottu_pais",
                        principalColumn: "Id_pais",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_gpsMottu_cidade",
                columns: table => new
                {
                    id_cidade = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nm_cidade = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    id_estado = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_gpsMottu_cidade", x => x.id_cidade);
                    table.ForeignKey(
                        name: "cidade_estado",
                        column: x => x.id_estado,
                        principalTable: "t_gpsMottu_estado",
                        principalColumn: "id_estado",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_gpsMottu_endereco",
                columns: table => new
                {
                    id_endereco = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    logradouro = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    numero = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false),
                    cep = table.Column<string>(type: "NVARCHAR2(8)", maxLength: 8, nullable: false),
                    id_cidade = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_gpsMottu_endereco", x => x.id_endereco);
                    table.ForeignKey(
                        name: "cidade_endereco",
                        column: x => x.id_cidade,
                        principalTable: "t_gpsMottu_cidade",
                        principalColumn: "id_cidade",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_gpsMottu_filial",
                columns: table => new
                {
                    id_filial = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    cnpj_filial = table.Column<string>(type: "NVARCHAR2(14)", maxLength: 14, nullable: false),
                    senha_filial = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    id_endereco = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    id_contato = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_gpsMottu_filial", x => x.id_filial);
                    table.ForeignKey(
                        name: "filial_endereco",
                        column: x => x.id_endereco,
                        principalTable: "t_gpsMottu_endereco",
                        principalColumn: "id_endereco",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_contato_filial",
                        column: x => x.id_contato,
                        principalTable: "t_gpsMottu_contato",
                        principalColumn: "id_contato",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_gpsMottu_moto",
                columns: table => new
                {
                    id_moto = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    status = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    condicao_manutencao_moto = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    identificador_moto = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    id_filial = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    id_tipo_moto = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_gpsMottu_moto", x => x.id_moto);
                    table.ForeignKey(
                        name: "FK_Moto_Filial",
                        column: x => x.id_filial,
                        principalTable: "t_gpsMottu_filial",
                        principalColumn: "id_filial",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Moto_TipoMoto",
                        column: x => x.id_tipo_moto,
                        principalTable: "t_gpsMottu_tipo_moto",
                        principalColumn: "id_tipo_moto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_gpsMottu_secoes_filial",
                columns: table => new
                {
                    id_secao_filial = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Lado_1 = table.Column<int>(type: "NUMBER(10)", maxLength: 100000, nullable: false, defaultValue: 0),
                    Lado_2 = table.Column<int>(type: "NUMBER(10)", maxLength: 100000, nullable: false, defaultValue: 0),
                    Lado_3 = table.Column<int>(type: "NUMBER(10)", maxLength: 100000, nullable: false, defaultValue: 0),
                    Lado_4 = table.Column<int>(type: "NUMBER(10)", maxLength: 100000, nullable: false, defaultValue: 0),
                    id_tipo_secao = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    id_filial = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_gpsMottu_secoes_filial", x => x.id_secao_filial);
                    table.ForeignKey(
                        name: "secao_filial",
                        column: x => x.id_filial,
                        principalTable: "t_gpsMottu_filial",
                        principalColumn: "id_filial",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "secao_tipoSecao",
                        column: x => x.id_tipo_secao,
                        principalTable: "t_gpsMottu_tipo_secao",
                        principalColumn: "id_tipo_secao",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_gpsMottu_usuario",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nm_usuario = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    ds_email = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    ds_senha = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    id_perfil = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    id_filial = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_gpsMottu_usuario", x => x.id_usuario);
                    table.ForeignKey(
                        name: "usuario_filial",
                        column: x => x.id_filial,
                        principalTable: "t_gpsMottu_filial",
                        principalColumn: "id_filial",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "usuario_perfil",
                        column: x => x.id_perfil,
                        principalTable: "t_gpsMottu_perfil",
                        principalColumn: "id_perfil",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_gpsMottu_uwb",
                columns: table => new
                {
                    id_uwb = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    id_moto = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    vl_iwb = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_gpsMottu_uwb", x => x.id_uwb);
                    table.ForeignKey(
                        name: "FK_t_gpsMottu_uwb_t_gpsMottu_moto_id_moto",
                        column: x => x.id_moto,
                        principalTable: "t_gpsMottu_moto",
                        principalColumn: "id_moto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_gpsMottu_cidade_id_estado",
                table: "t_gpsMottu_cidade",
                column: "id_estado");

            migrationBuilder.CreateIndex(
                name: "IX_t_gpsMottu_contato_id_Telefone",
                table: "t_gpsMottu_contato",
                column: "id_Telefone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_gpsMottu_endereco_id_cidade",
                table: "t_gpsMottu_endereco",
                column: "id_cidade");

            migrationBuilder.CreateIndex(
                name: "IX_t_gpsMottu_estado_id_pais",
                table: "t_gpsMottu_estado",
                column: "id_pais");

            migrationBuilder.CreateIndex(
                name: "IX_t_gpsMottu_filial_id_contato",
                table: "t_gpsMottu_filial",
                column: "id_contato");

            migrationBuilder.CreateIndex(
                name: "IX_t_gpsMottu_filial_id_endereco",
                table: "t_gpsMottu_filial",
                column: "id_endereco");

            migrationBuilder.CreateIndex(
                name: "IX_t_gpsMottu_moto_id_filial",
                table: "t_gpsMottu_moto",
                column: "id_filial");

            migrationBuilder.CreateIndex(
                name: "IX_t_gpsMottu_moto_id_tipo_moto",
                table: "t_gpsMottu_moto",
                column: "id_tipo_moto");

            migrationBuilder.CreateIndex(
                name: "IX_t_gpsMottu_secoes_filial_id_filial",
                table: "t_gpsMottu_secoes_filial",
                column: "id_filial");

            migrationBuilder.CreateIndex(
                name: "IX_t_gpsMottu_secoes_filial_id_tipo_secao",
                table: "t_gpsMottu_secoes_filial",
                column: "id_tipo_secao");

            migrationBuilder.CreateIndex(
                name: "IX_t_gpsMottu_usuario_id_filial",
                table: "t_gpsMottu_usuario",
                column: "id_filial");

            migrationBuilder.CreateIndex(
                name: "IX_t_gpsMottu_usuario_id_perfil",
                table: "t_gpsMottu_usuario",
                column: "id_perfil");

            migrationBuilder.CreateIndex(
                name: "IX_t_gpsMottu_uwb_id_moto",
                table: "t_gpsMottu_uwb",
                column: "id_moto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_gpsMottu_secoes_filial");

            migrationBuilder.DropTable(
                name: "t_gpsMottu_telefone");

            migrationBuilder.DropTable(
                name: "t_gpsMottu_usuario");

            migrationBuilder.DropTable(
                name: "t_gpsMottu_uwb");

            migrationBuilder.DropTable(
                name: "t_gpsMottu_tipo_secao");

            migrationBuilder.DropTable(
                name: "t_gpsMottu_perfil");

            migrationBuilder.DropTable(
                name: "t_gpsMottu_moto");

            migrationBuilder.DropTable(
                name: "t_gpsMottu_filial");

            migrationBuilder.DropTable(
                name: "t_gpsMottu_tipo_moto");

            migrationBuilder.DropTable(
                name: "t_gpsMottu_endereco");

            migrationBuilder.DropTable(
                name: "t_gpsMottu_contato");

            migrationBuilder.DropTable(
                name: "t_gpsMottu_cidade");

            migrationBuilder.DropTable(
                name: "t_gpsMottu_estado");

            migrationBuilder.DropTable(
                name: "t_gpsMottu_pais");
        }
    }
}
