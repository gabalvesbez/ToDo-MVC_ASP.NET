# Projeto To-Do List em ASP.NET Core MVC

# Feito por Cristian Grigorita, Gabriel Bezerra e  Guilherme Barbosa

## Instruções para Restaurar o Backup da Base de Dados

1. Abra o SQL Server Management Studio (SSMS).
2. Conecte-se ao servidor de banco de dados.
3. No Object Explorer, clique com o botão direito no nó 'Databases' e selecione 'Restore Database...'.
4. Na janela de restauração, selecione 'Device' e clique no botão '...' para selecionar o arquivo de backup.
5. Clique em 'Add' e navegue até o local onde o arquivo de backup está salvo (por exemplo, 'DiretorioExemplo\ToDoDBBackup.bak').
6. Selecione o arquivo de backup e clique em 'OK'.
7. Configure as opções de restauração conforme necessário e clique em 'OK' para iniciar a restauração.

## Atualizar a String de Conexão

Certifique-se de atualizar a string de conexão no arquivo 'appsettings.json' e/ou 'Program.cs' para apontar para o servidor de banco de dados correto.

## Executar o Projeto

1. Abrir o projeto no Visual Studio.
2. Restaurar os pacotes NuGet.
 2.1 Na barra superior, clicar em Ferramentas > Gerenciador de Pacotes do NuGet > Console do Gerenciador de Pacotes > Na linha de comando digitar 'Update-Database'
3. Compilar e executar o projeto.
