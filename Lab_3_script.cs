using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class GameScript : MonoBehaviour
{
    public Button backbtn;
    public GameObject panel;
    public Text winText;
    public GameObject Cross, Nought;
    public GameObject[] squares;
    public Text ins;
    public enum Seed { EMPTY, CROSS, NOUGHT };
    public Seed turn;
    private char[,] board;
    private GameInitializationRules gameObject;
    int difficulty ;
    bool clicksActive = true;
    public void Awake()
    {
        
        // ins.text = "Turn: " + turn.ToString();
        GameObject persistantObj = GameObject.FindGameObjectWithTag("PersistantObj") as GameObject;
        difficulty = persistantObj.GetComponent<PersistanceScript>().difficulty;

        initiateGame();

    }
    bool checkWinner(){
        int score = winConditions(board,gameObject);
        if(score == gameObject.getUnitScore()){
            //bot won
            showPanel("Bot Won");
            PlayerPrefs.SetInt("loses",PlayerPrefs.GetInt("loses")+1);
            return false;
        }else if(score == -gameObject.getUnitScore()){
            showPanel("Player Won");
            PlayerPrefs.SetInt("wins",PlayerPrefs.GetInt("wins")+1);
            return true;
        }
        else{
            if(!isThereMovesLeft(board,gameObject)){
                showPanel("DRAW");
                return false;
            }
            return false;
        }
        
        
    }
    void showPanel(string winner){
        panel.SetActive(true);
        winText.text=winner;
        clicksActive = false;
        Destroy(backbtn);
    }
    public void initiateGame()
    {
        gameObject = new GameInitializationRules('o', 'x', '_');
        
        // 0 easy, 2 kinda easy, 3 bugged, 4 medium, 5 rational
        board = resetBoard();

    }
    public void SpawnNew(GameObject obj, int square_index)
    {
        if(!clicksActive){
            return;
        }
        if (turn == Seed.CROSS)
        {
            Instantiate(Cross, obj.transform.position, Quaternion.identity);
            // turn = Seed.NOUGHT;
        }
        else
        {
            Instantiate(Nought, obj.transform.position, Quaternion.identity);
            // turn = Seed.CROSS;
        }
        Destroy(obj.gameObject);

        // ins.text = "Turn: " + turn.ToString();
        playInPlace(square_index);
    }
    void playInPlace(int square_index)
    {
        int col = ((square_index % 3) + 2) % 3;
        int row = (int)Mathf.Floor((square_index - 1) / 3.0F);
        // Debug.Log("Player Move   " + row + " " + col);
        // Debug.Log("player character: "+gameObject.getPlayer());
        board[row, col] = gameObject.getPlayer();

        if(checkWinner())
            return;

        if (isThereMovesLeft(board, gameObject))
        {
            // Debug.Log("ADAWDAWD");
            playBot();
            checkWinner();
        }
    }
    void playBot()
    {
        board = botPlay();

    }
    char[,] resetBoard()
    {
        char[,] b = { { '_', '_', '_' }, { '_', '_', '_' }, { '_', '_', '_' } };
        return b;
    }
    //  movesleft?
    bool isThereMovesLeft(char[,] board, GameInitializationRules gameObject)
    {
        char blankspace = gameObject.getBlankspace();
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j] == blankspace)
                    return true;
            }
        }
        return false;
    }

    // evaluation  
    int winConditions(char[,] board, GameInitializationRules gameObject)
    {
        char bot = gameObject.getBot();
        int unitScore = gameObject.getUnitScore();
        char player = gameObject.getPlayer();
        char blankspace = gameObject.getBlankspace();
        // check rows
        for (int i = 0; i < board.GetLength(0); i++)
        {
            if (board[i, 0] == board[i, 1] && board[i, 0] == board[i, 2]) // && board[i, 0] != blankspace
            {
                if (board[i, 0] == bot)
                    return unitScore;
                else if (board[i, 0] == player)
                    return -unitScore;
            }
        }
        // check columns
        for (int i = 0; i < board.GetLength(0); i++)
        {
            if (board[0, i] == board[1, i] && board[0, i] == board[2, i])// && board[0,i] != blankspace
            {
                if (board[0, i] == bot)
                    return unitScore;
                else if (board[0, i] == player)
                    return -unitScore;
            }
        }
        // check diagonal
        if (board[0, 0] == board[1, 1] && board[0, 0] == board[2, 2]) // && board[0, 0] != blankspace
        {
            if (board[0, 0] == bot)
                return unitScore;
            else if (board[0, 0] == player)
                return -unitScore;
        }
        // check other diagonal
        if (board[0, 2] == board[1, 1] && board[2, 0] == board[1, 1])// && board[0, 2] != blankspace
        {
            if (board[0, 2] == bot)
                return unitScore;
            else if (board[0, 2] == player)
                return -unitScore;
        }
        // no winner yet   
        return 0;
    }
    int minimaxwithalphabeta(bool isMax, char[,] board, int alpha, int beta, int depth)
    {
        Debug.Log("Difficuly "+difficulty);
        char bot = gameObject.getBot();
        char player = gameObject.getPlayer();
        int unitScore = gameObject.getUnitScore();
        char blankspace = gameObject.getBlankspace();
        int score = winConditions(board, gameObject);
        if (depth >= difficulty){
            return score;
        }
        // check if player or opponent won the game
        if (score == gameObject.getUnitScore())
        {
            return score - depth;
        }
        else if (score == -gameObject.getUnitScore())
        {
            return score + depth;
        }
        // if no moves left, end with a draw
        if (!isThereMovesLeft(board, gameObject))
        {
            return 0;
        }
        int myscore;
        if (isMax)
        {
            //bot
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == blankspace)
                    {
                        board[i, j] = bot;
                        myscore = minimaxwithalphabeta(!isMax, board, alpha, beta, depth + 1);
                        board[i, j] = blankspace;

                        if (myscore > alpha)
                            alpha = score;
                        if (alpha > beta)
                            break;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == blankspace)
                    {
                        board[i, j] = player;
                        myscore = minimaxwithalphabeta(!isMax, board, alpha, beta, depth + 1);
                        board[i, j] = blankspace;

                        if (myscore < beta)
                            beta = score;
                        if (alpha > beta)
                            break;
                    }
                }
            }
        }
        return beta;

    }
    int minimax(char[,] board, int depth, bool isMax, GameInitializationRules gameObject, int alpha, int beta)
    {

        char bot = gameObject.getBot();
        char player = gameObject.getPlayer();
        int unitScore = gameObject.getUnitScore();
        char blankspace = gameObject.getBlankspace();

        int score = winConditions(board, gameObject);
        
        // check if player or opponent won the game
        if (score == gameObject.getUnitScore())
        {
            return score - depth;
        }
        else if (score == -gameObject.getUnitScore())
        {
            return score + depth;
        }
        if (depth >= difficulty){
            return score;
        }

        // if reached here then no one won yet

        // if no moves left, end with a draw
        if (!isThereMovesLeft(board, gameObject))
        {
            return 0;
        }

        //best value
        if (isMax)
        { //BOT maximizer
            int bestvalue = int.MinValue;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == blankspace)
                    {
                        board[i, j] = bot;

                        bestvalue = Mathf.Max(bestvalue, minimax(board, depth + 1, !isMax, gameObject,alpha,beta));
                        
                        // undo the move
                        board[i, j] = blankspace;
                        if(bestvalue > alpha)
                        alpha = bestvalue;
                        if(alpha> beta)
                        break;
                    }
                }
            }
            // Debug.Log("AAAAAA THE BEST VALUE IS   " + bestvalue);
            return bestvalue;

        }
        else
        {// Player minimizer

            int bestvalue = int.MaxValue;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == blankspace)
                    {
                        board[i, j] = player;

                        bestvalue = Mathf.Min(bestvalue, minimax(board, depth + 1, !isMax, gameObject,alpha,beta));

                        // undo the move
                        board[i, j] = blankspace;

                        if(bestvalue < beta)
                        beta = bestvalue;
                        if(alpha> beta)
                        break;
                    }
                }
            }
            // Debug.Log("BBBBBB THE BEST VALUE IS   " + bestvalue);
            return bestvalue;
        }
    }
    Move findBestMove(char[,] board, GameInitializationRules gameObject)
    {
        char bot = gameObject.getBot();
        char player = gameObject.getPlayer();
        int unitScore = gameObject.getUnitScore();
        char blankspace = gameObject.getBlankspace();
        // assuming im calculating the best move for the BOT (maxmization)
        int bestvalue = int.MinValue;
        Move bestMove = new Move(-1, -1);
        int moveValue = 0;

        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                // char = board[row][col]
                if (board[i, j] == blankspace)
                {
                    // Debug.Log("A7eeeh   "+i+"  "+j +"  bool  "+(board[i, j] == blankspace)+"  blankspace  "+blankspace+"  board "+board[i, j]);
                    board[i, j] = bot;
                    moveValue = minimax(board, 0, false, gameObject,-1000,1000);
                    // moveValue = minimaxwithalphabeta(false,board,-1000,1000,0);

                    board[i, j] = blankspace;

                    // max (BOT)
                    if (moveValue > bestvalue)
                    {
                        bestvalue = moveValue;
                        bestMove.col = j;
                        bestMove.row = i;
                    }

                }
            }
        }

        return bestMove;
    }
    char[,] botPlay()
    {
        Move bestMove = findBestMove(board, gameObject);
        Debug.Log("AI move    " + bestMove.row + " " + bestMove.col + "    character: " + gameObject.getBot());

        board[bestMove.row, bestMove.col] = gameObject.getBot();
        renderObject(bestMove);
        return board;
    }
    void renderObject(Move bestMove)
    {

        int myindex = 0;
        switch (bestMove.row)
        {
            case 0:
                switch (bestMove.col)
                {
                    case 0:
                        myindex = 0;
                        break;
                    case 1:
                        myindex = 1;
                        break;
                    case 2:
                        myindex = 2;
                        break;
                }
                break;
            case 1:
                switch (bestMove.col)
                {
                    case 0:
                        myindex = 3;
                        break;
                    case 1:
                        myindex = 4;
                        break;
                    case 2:
                        myindex = 5;
                        break;
                }
                break;
            case 2:
                switch (bestMove.col)
                {
                    case 0:
                        myindex = 6;
                        break;
                    case 1:
                        myindex = 7;
                        break;
                    case 2:
                        myindex = 8;
                        break;
                }
                break;
        }
        GameObject obj = squares[myindex];
        if (turn != Seed.CROSS)
        {
            Instantiate(Cross, obj.transform.position, Quaternion.identity);
            // turn = Seed.NOUGHT;
        }
        else
        {
            Instantiate(Nought, obj.transform.position, Quaternion.identity);
            // turn = Seed.CROSS;
        }
        Destroy(obj.gameObject);
        ins.text = "Turn: " + turn.ToString();

    }
}
class Move
{
    public int row, col;
    public Move(int row, int col)
    {
        this.row = row;
        this.col = col;
    }
}
class GameInitializationRules
{
    char bot = 'o', player = 'x', blankspace = '_';
    int unitScore = 10;

    public GameInitializationRules(char bot, char player, char blankspace)
    {
        this.bot = bot;
        this.player = player;
        this.blankspace = blankspace;
    }
    public char getBot()
    {
        return bot;
    }
    public char getPlayer()
    {
        return player;
    }
    public char getBlankspace()
    {
        return blankspace;
    }
    public int getUnitScore()
    {
        return unitScore;
    }
}