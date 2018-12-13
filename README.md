Técnicas/Linguagem utilizada:
===========================================
> AspNetCore 2.1, C#
> SOLID
> Repository Pattern
> IoC

Documentação:
===========================================
> Swagger

Executando a aplicação
===========================================
A aplicação trata-se de um web api RESTFULL 

URL APIs: http://localhost:5000/swagger


Chamada via CURL
===========================================
curl http://localhost:5000/api/v1/feeds/topics


EXTRA: Banco de dados Mongo
===========================================
Ao consumir o serviço Baixar Feeds, um "espelho" do registros é criado em uma base de dados mongo. O objetivo é mostrar de forma simples como a aplicação escalaria de foma simples, caso fosse necessário manter uma base de dados local para outras análises.

Credenciais do banco de dados:
mongodb://msdbuser:ms#2018@ds135592.mlab.com:35592/minutosegurodb


Fontes para algumas informações:
===========================================
https://www.normaculta.com.br/artigo-indefinido/
https://www.normaculta.com.br/artigo-definido/
https://www.normaculta.com.br/preposicao/

