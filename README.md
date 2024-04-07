# Galo da Velha

## Autoria

Trabalho realizado por:
- Afonso Cunha - a22302960
  - Responsável por:
    - Código da Classe Piece;
    - Relatório Markdown
    - Enum PieceTraits;
- Júlia Costa - a22304403
  - Responsável por:
    - Código da Classe Piece;
    - Código da Classe GameBoard;
    - Código da Classe GameManager;
    - Enum XCoords;
    - Documentação das Classes GameBoard e Piece;

[URL para o repositório Git](https://github.com/Juhhxx/GaloDaVelha_LP1)

## Arquitetura da solução

### Descrição

Para este projeto, escolhemos dividi-lo em 3 classes diferentes e ainda duas enumerações.

É relevante também referir que este programa corre em qualquer consola, mas é preferível o Windows Powershell e a consola do Visual Studio, pois apenas nestes foi possível representar as cores adequadamente, assim como os caracteres Unicode escolhidos.

Comçando pelo Main() dá-se uma instanciação de um objeto GameManager, chamando de seguida o método GameStart()
1. **Class GameManager**
   - Responsável por "gerir" o jogo à qual pertencem os métodos:
      - **GameSetup()** - instancia um novo objeto da classe GameBoard, estabelecendo as condições referentes ao estado zero do jogo;
      - **PlayAgain()** - imprime no ecrã a questão se o jogador quer jogar outra vez e reinicia o jogo caso o jogador responda "y" e sai do game loop se o jogador escrever "n";
      - **GameStart()** - chama o GameSetup() e inicia o game loop, onde se chama o AskForInputs() e, após uma vitória ou empate chama o PlayAgain();
2. **Class Piece**
     - Responsável por métodos associados à instanciação de peças, assim como as suas características:
       - **InitializePiece()** - inicializa o código de uma peça e verific se a peça é válida;
       - **Decode(string code)** - recebendo uma string do código correspondente a uma peça, configura os traits da instância da classe Piece com recurso à Enum PieceTraits, retornando o código Enum que representa esta instância;
       - **InArray(string piece)** - recebendo uma string (obtida através do **GetTrait()**) que contém os traits da peça, verifica se essa "peça" está já registada no array que tem como objetivo guardar um log das peças instanciadas, retorna um booleano;
       - **SetName()** - usando um array tridimensional, categoriza cada peça de acordo com os seus traits (exceto a cor) e atribui-lhe um unicode correspondente ao símbolo que representará a peça;
       - **SetColor()** - verifica se o código da peça contém o Enum color ativo, ditando com que cor o símbolo da peça vai ser impresso no ecrã (magenta para "light" e vermelho para "dark");
       - **GetTrait()** - constroi e retorna uma string que contem os vários traits da instancia da peça;
       - **GetName()** - retorna o caracter Unicode que representará a peça;
       - **GetColor()** - retorna um ConsoleColor, referente à  cor da peça;
       - **CheckForTrait(PieceTraits trait, string res1, string res2)** - verifica se a trait (e.x big) nessa peça está ativa ou não, se a trait tiver ativa, devolve a string correspondente ao trait ativo ("big") e s tiver iantivo devolve a string para o trait inativo ("tiny");
       - **ResetPiecesArray()** - manda reset ao array que regista as peças criadas deixando-o "vazio" e pronto para um novo jogo;
3. **Class GameBoard**
    - Reponsável pela interface e ações de jogadores assim como verificação de estado do jogo:
      - **AskForInputs(int gameTurn)** - chama os métoos **AskForPiece(gametTurn)** e **AskForCoords(gameTurn)** e insere a peça na matriz tabuleiro, atualizando o board e o infoBoard. É também responsável por chamar o CheckForGameWin() e imprimirá o resultado adequado caso algum dos jogadores ganhe ou em caso de empate;
      - **WhoPlays()** - responsávl por verificar quem é que seleciona a peça a colocar pelo outro jogador naquele turno;
      - **AskForPiece(int gameTurn)** - pede pelo input de um código de traits correspondente a uma peça que o jogador quer que o outro jogador coloque futuramente, retorna a peça desejada pelo utilizador;
      - **AskForCoords(int gameTurn)** - pede pelo input de coordenadas do tabuleiro (e.x "A3) onde o jogador deseja colocar a peça, retorna um array de inteiros correspondentes às coordenadas desejadas;
      - **CheckCoordInRange(string coord)** - recebe a string correspondente às coordenadas onde o jogador deseja colocar a peça e verifica se essas coordenadas existem no tabuleiro, retornando um booleano;
      - **CoordIsEmpty(int[] coord)** - recebe um array com inteiros correspondentes às coordenadas onde o jogador deseja colocar a peça e verifica se esse espaço já está ocupado por outra peça, retorna um booleano;
      - **PrintBoard()** - imprime o tabuleiro na consola;
      - **ColoredText(string str, ConsoleColor color)** - imprime uma string com a cor especificada; 
      - **CheckXWin(int x, int y, int traitCheck)** - usa as coordenadas da última pça colocada e retorna true se verificar uma condição de vitória naquela linha,  caso contrário retorna false;
      - **CheckYWin(int x, int y, int traitCheck)** - usa as coordenadas da última pça colocada e retorna true se verificar uma condição de vitória naquela coluna,  caso contrário retorna false;
      - **CheckDiagonalPosWin(int x, int y, int traitCheck)** - retorna true se se verificar uma condição de vitória na diagonal A1 - D4, caso contrário retorna false;
      - **CheckDiagonalNegWin(int x, int y, int traitCheck)** - retorna true se se verificar uma condição de vitória na diagonal D1 - A4, caso contrário retorna false;
      - **CheckForGameWin(int[] lastCoords)** - Chama cada um dos 4 métodos CheckWin e retorna um booleano onde true significa que alguém ganhou o jogo e false significa que não foi verificada nenhuma condição de vitória; 
4. **Enum PieceTraits**
    - Enumeração bit a bit que contém os vários tipos de traits que uma peça pode ter;
5. **Enum XCoords**
    - Enumeração que contém uma correspondência entre letras (A,B,C,D) e valores inteiros (1,2,3,4 correspondentemente) de forma a numerar o eixo horizontal do tabuleiro, perservando uma nomenclatura clássica vista em vários tabuleiros;

### Fluxograma

```mermaid
graph TB
A(["Start"]) --> C("Zero state board is set")
    C -- Game loop begins --> D{Is win condition\nor draw detected?}
    subgraph MainLoop
        D -- No --> F[["Ask for Inputs"]]
        subgraph Inputs
        F --- G[/"Ask for a Piece"/]
        G --> K{"Piece already on board?"} 
        K -- No --> I[/"Ask for Coords"/]
        K --Yes --> G
        I --> Q{Is the coordinate\n within the board and unnocuppied?}
        end
        Q -- Yes --> M[[Place Piece on Board]]
        Q -- No --> I
        M --> J[Increment game turn] --> D
    end
    
    D -- Yes --> Z(Print result) 
    Z --> E("Play again?")
    E -- Yes --> C
    E -- No --> P([End]) 
```

## Referências

Neste projeto recorreu-se a [esta biblioteca de códigos Unicode](https://symbl.cc/en/) e foi utilizada AI em contexto de estudo da sintaxe de Mermaid. Tirando esta referência, não foram realizadas trocas de ideias com colegas nem foram utilizados pedaços de código gerado por IAs generativas ou outros pedaçoes de código aberto ou bibliotecas de terceiros.