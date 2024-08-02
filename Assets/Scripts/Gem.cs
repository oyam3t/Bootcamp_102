using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Gem : MonoBehaviour
{
    public int col;
    public int row;
    private Board board;
    private GameObject otherGem;
    public matchfinder matchfinder;
    public bool matched = false;

    public int prevCol;
    public int prevRow;
    public int tarX;
    public int tarY;
    public Vector2 tempPos;

    private Vector2 ftp;
    private Vector2 etp;
    public float angle = 0;
    public float resist = 1f;

    // Start is called before the first frame update
    void Start()
    {
        board = FindAnyObjectByType<Board>();
        matchfinder = FindAnyObjectByType<matchfinder>();
        //tarX = (int)transform.position.x;
        //tarY = (int)transform.position.y;
        //col = tarX;
        //row = tarY;
        //prevCol = col;
        //prevRow = row;
    }

    // Update is called once per frame
    void Update()
    {
        //FindMatch();
        if (matched)
        {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            sprite.color = new Color(0f,0f,0f,.2f);
        }
        tarX = col;
        tarY = row;
        if (Mathf.Abs(tarX - transform.position.x) > .1)
        {
            //Move Towards the target
            tempPos = new Vector2(tarX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPos, .4f);
            if (board.allGems[col, row] != this.gameObject)
            {
                board.allGems[col, row] = this.gameObject;
            }
            matchfinder.FindAllMatches();
        }
        else
        {
            //Directly set the position
            tempPos = new Vector2(tarX, transform.position.y);
            transform.position = tempPos;
            //board.allGems[col,row] = this.gameObject;
        }

        if (Mathf.Abs(tarY - transform.position.y) > .1)
        {
            //Move Towards the target
            tempPos = new Vector2(transform.position.x, tarY);
            transform.position = Vector2.Lerp(transform.position, tempPos, .6f);
            if (board.allGems[col, row] != this.gameObject)
            {
                board.allGems[col, row] = this.gameObject;
            }
            matchfinder.FindAllMatches();

        }
        else
        {
            //Directly set the position
            tempPos = new Vector2(transform.position.x, tarY);
            transform.position = tempPos;
            //board.allGems[col, row] = this.gameObject;
        }




    }

    private void OnMouseDown()
    {
        if(board.currentState == GameState.move)
        {
            ftp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void OnMouseUp()
    {
        if (board.currentState == GameState.move)
        {
            etp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            calcAngle();
        }
        else
        {
            board.currentState = GameState.move;
        }
    }

    void calcAngle()
    {
        if (Mathf.Abs(ftp.x - etp.x) > resist || Mathf.Abs(ftp.y - etp.y) > resist)
        {
            angle = Mathf.Atan2(etp.y - ftp.y, etp.x - ftp.x) * 180 / Mathf.PI;
            MovePieces();
            board.currentState = GameState.wait;
        }
    }

    void MovePieces()
    {
        this.GetComponent<AudioSource>().Play();
        if (angle > -45 && angle <= 45 && col < board.width - 1)
        {
            //Right Swipe
            otherGem = board.allGems[col + 1, row];
            prevRow = row;
            prevCol = col;
            otherGem.GetComponent<Gem>().col -=1;
            col += 1;
            //StartCoroutine(CheckMoveCo());
            
            //MovePiecesActual(Vector2.right);
        }
        else if (angle > 45 && angle <= 135 && row < board.height - 1)
        {
            //Up Swipe
            otherGem = board.allGems[col, row + 1];
            prevRow = row;
            prevCol = col;
            otherGem.GetComponent<Gem>().row -=1;
            row += 1;
            //StartCoroutine(CheckMoveCo());
            //MovePiecesActual(Vector2.up);
        }
        else if ((angle > 135 || angle <= -135) && col > 0)
        {
            //Left Swipe
            otherGem = board.allGems[col - 1, row];
            prevRow = row;
            prevCol = col;
            otherGem.GetComponent<Gem>().col +=1;
            col -= 1;
            //StartCoroutine(CheckMoveCo());
            //MovePiecesActual(Vector2.left);
        }
        else if (angle < -45 && angle >= -135 && row > 0)
        {
            //Down Swipe
            otherGem = board.allGems[col, row - 1];
            prevRow = row;
            prevCol = col;
            otherGem.GetComponent<Gem>().row +=1;
            row -= 1;
            //StartCoroutine(CheckMoveCo());
            //MovePiecesActual(Vector2.down);
        }
        else
        {

            //board.currentState = GameState.move;
        }
        StartCoroutine(checkMoveCoroutine());
    }

    public IEnumerator checkMoveCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        if (otherGem != null)
        {
            if (!matched && !otherGem.GetComponent<Gem>().matched)
            {
                otherGem.GetComponent<Gem>().row = row;
                otherGem.GetComponent<Gem>().col = col;
                row = prevRow;
                col = prevCol;
                yield return new WaitForSeconds(.5f);
                //board.currentDot = null;
                board.currentState = GameState.move;
            }
            else
            {
                board.DestroyMatches();
                
            }
            otherGem = null;
        }
    }

    void FindMatch()
    {
        if (col > 0 && col < board.width - 1)
        {
            GameObject leftDot1 = board.allGems[col - 1, row];
            GameObject rightDot1 = board.allGems[col + 1, row];
            if (leftDot1 != null && rightDot1 != null)
            {
                if (leftDot1.tag == this.gameObject.tag && rightDot1.tag == this.gameObject.tag)
                {
                    leftDot1.GetComponent<Gem>().matched = true;
                    rightDot1.GetComponent<Gem>().matched = true;
                    matched = true;
                }
            }
        }
        if (row > 0 && row < board.height - 1)
        {
            GameObject upDot1 = board.allGems[col, row + 1];
            GameObject downDot1 = board.allGems[col, row - 1];
            if (upDot1 != null && downDot1 != null)
            {
                if (upDot1.tag == this.gameObject.tag && downDot1.tag == this.gameObject.tag)
                {
                    upDot1.GetComponent<Gem>().matched = true;
                    downDot1.GetComponent<Gem>().matched = true;
                    matched = true;
                }
            }
        }
    }
}
