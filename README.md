Mais um projeto pra treinar .NET 

Nesse projeto o objetivo é criar uma pequena loja virtual de livros.

O usuário faz login na aplicação e tem uma variedade de livros pra colocar no carrinho e comprar.

Nesse primeiro commit, já temos 90% da parte de infraestrutura pronta, models de Livro/Categoria/Autor, Unity Of Work, Repository Pattern...
Na pasta "processos" temos a representação dos processos que acontecem por debaixo dos panos, no cadastro de um livro é feito um insert numa tabela chamada LivroAutor, 
que faz a referência entre um Autor e um Livro.
Controllers e Views de Livro, Autor e Categoria ok

ToDo: Implementar a loja e os processos que serão realizados





//////////
EN:

Another simple project to practice dotNet

The main goal of this project is to create a small book store.


In this first commit, we already have almost 90% of our infra complete. Books/Category/Authors models, Unity Of Work, Repository Pattern...
In "Processos" paste we have all the processment that will happen, for example: when the user 'create' a book, the app send a insert to a Table called 'Livro' and to another
table called 'LivroAutor' who is created just to register the references in between two entitys.
Controllers and Views - done
