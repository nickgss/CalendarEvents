Aplica��o Simples de Cadastro de  Eventos (CRUD)
	Instala��o do Aplicativo:
	Executar Script em Docs/CRUD_Calendar_Events.sql do DATABASE em um DB Server.
	
	Configurar arquivo de conexao em : 
		Projeto => CalendarEvent/App.config
		Tags : <connectionStrings > : Esta tag cont�m as configura��es de Conex�o com o Banco de Dados
				<appSettings> Essa Tag contem o Provider do Banco de Dados ou seja se nao for um banco de dados em SQL Server deve especificar qual provider usar.


	Ap�s as configura��es acima basta compilar o projeto no Visual Studio 2013 ou de vers�o mais recente.

Especifica��es do CRUD:
Primeiramente o CRUD Possui 2 Projetos :
1�:
	Calendar_Event : Simplesmente a aplica��o principal do CRUD. Onde se localiza toda a l�gica e pr�tica da aplica��o. Onde temos 
	Este CRUD foi desenvolvido usando um padr�o de desenvolvimento da Microsoft, Model-View-ViewModel (MVVM), o aplicativo foi desenvolvido usando a Tecnologia da Microsoft Windows Presentation Fundation (WPF), 
	tendo base nesse padr�o, onde temos as seguintes Camadas:
	A camada de Modelo : (Model) onde estao as classes de modelo do nosso CRUD. Tamb�m usada para se comunicar com o DAO que manipula os objetos da aplica��o no Database;
	A Camada View : E a interface grafica do usu�rio as telas, onde se usa a linguagem de marca��o XAML bem parecida com a XML onde possui tamb�m valida��es Front-End usando CodeBehind;
	A Camada ViewModel : Esta camada obtem a maior parte do conteudo do CRUD porque esta camada e responssavel por realizar a comunica��o entre as camadas View e Model e responsavel tambem
	por realizar chamadas da camada Model para a View e da View para o Model.
	
	Pasta do projeto Utils e ApplicationService : Estas pastas foram criadas como uma forma de auxilio a pasta Utils contem arquivos de classes utilit�rio onde temos os DataBind de PasswordBox para String
	e a pasta ApplicationService e usada para classes de auxilio de navega��o das Views onde tenho interfaces para navega��o entre as views.
2�:
	DAO : Uso esta DLL como se fosse um framework para auxiliar a realizar as tarefas de conex�o com o banco de dados, usando propriedades da classe : DbProviderFactory, na cria��o de conex�es
	Optei por usar apenas por organiza��o de c�digo mesmo. Nesta aplica��o uso a ORM ADO.Net uma interface de comunica��o com o banco de dados, optei por usar ADO.Net pra mostrar o controle
	das queries que estou executanto no banco de dados, tambem possuo conhecimento com Entity FrameWork (EF).



	* TRABALHOS FUTUROS


	1- Implementar valida��es de datas de eventos onde :
		1 : A data do evento � valida?
		2 : A data final do evento e maior que a data de inicio do evento.
		3 : Valida��o se h� ou n�o eventos no mesmo dia onde se houver o usu�rio nao poder� participar dos dois ao mesmo tempo.

	2 - Implementar janela de calend�rio onde mostre os respectivos eventos no calend�rio (Onde ja possui uma view de prot�tipo dentro do projeto)
	3 - Implmentar sistema mult-usuarios : Onde cada usu�rio ter� sua sess�o de eventos, os eventos dever�o ser identificados por cada usu�rio propriet�rio de seu respectivo cliente.
	4 - Implmentar uma interface mais completa.( Pois esta eu reconheco que � o prot�tipo do prot�tipo)
	5 - Implementa��o de intera��o dos usu�rios onde um usu�rio possa convidar o outro para seu repectivo cliente.