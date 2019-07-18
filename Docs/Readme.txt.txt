Aplicação Simples de Cadastro de  Eventos (CRUD)
	Instalação do Aplicativo:
	Executar Script em Docs/CRUD_Calendar_Events.sql do DATABASE em um DB Server.
	
	Configurar arquivo de conexao em : 
		Projeto => CalendarEvent/App.config
		Tags : <connectionStrings > : Esta tag contém as configurações de Conexão com o Banco de Dados
				<appSettings> Essa Tag contem o Provider do Banco de Dados ou seja se nao for um banco de dados em SQL Server deve especificar qual provider usar.


	Após as configurações acima basta compilar o projeto no Visual Studio 2013 ou de versão mais recente.

Especificações do CRUD:
Primeiramente o CRUD Possui 2 Projetos :
1º:
	Calendar_Event : Simplesmente a aplicação principal do CRUD. Onde se localiza toda a lógica e prática da aplicação. Onde temos 
	Este CRUD foi desenvolvido usando um padrão de desenvolvimento da Microsoft, Model-View-ViewModel (MVVM), o aplicativo foi desenvolvido usando a Tecnologia da Microsoft Windows Presentation Fundation (WPF), 
	tendo base nesse padrão, onde temos as seguintes Camadas:
	A camada de Modelo : (Model) onde estao as classes de modelo do nosso CRUD. Também usada para se comunicar com o DAO que manipula os objetos da aplicação no Database;
	A Camada View : E a interface grafica do usuário as telas, onde se usa a linguagem de marcação XAML bem parecida com a XML onde possui também validações Front-End usando CodeBehind;
	A Camada ViewModel : Esta camada obtem a maior parte do conteudo do CRUD porque esta camada e responssavel por realizar a comunicação entre as camadas View e Model e responsavel tambem
	por realizar chamadas da camada Model para a View e da View para o Model.
	
	Pasta do projeto Utils e ApplicationService : Estas pastas foram criadas como uma forma de auxilio a pasta Utils contem arquivos de classes utilitário onde temos os DataBind de PasswordBox para String
	e a pasta ApplicationService e usada para classes de auxilio de navegação das Views onde tenho interfaces para navegação entre as views.
2º:
	DAO : Uso esta DLL como se fosse um framework para auxiliar a realizar as tarefas de conexão com o banco de dados, usando propriedades da classe : DbProviderFactory, na criação de conexões
	Optei por usar apenas por organização de código mesmo. Nesta aplicação uso a ORM ADO.Net uma interface de comunicação com o banco de dados, optei por usar ADO.Net pra mostrar o controle
	das queries que estou executanto no banco de dados, tambem possuo conhecimento com Entity FrameWork (EF).



	* TRABALHOS FUTUROS


	1- Implementar validações de datas de eventos onde :
		1 : A data do evento é valida?
		2 : A data final do evento e maior que a data de inicio do evento.
		3 : Validação se há ou não eventos no mesmo dia onde se houver o usuário nao poderá participar dos dois ao mesmo tempo.

	2 - Implementar janela de calendário onde mostre os respectivos eventos no calendário (Onde ja possui uma view de protótipo dentro do projeto)
	3 - Implmentar sistema mult-usuarios : Onde cada usuário terá sua sessão de eventos, os eventos deverão ser identificados por cada usuário proprietário de seu respectivo cliente.
	4 - Implmentar uma interface mais completa.( Pois esta eu reconheco que é o protótipo do protótipo)
	5 - Implementação de interação dos usuários onde um usuário possa convidar o outro para seu repectivo cliente.