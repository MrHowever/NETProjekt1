package game;

import GUI.Gui;
import clocks.GameClock;

import java.awt.*;
import java.util.ArrayList;

import static game.Dir.*;

public class Snake
{
    public static boolean waitToMove = false;
    public boolean aiContrrolled = false;

    public int ID;

    public  Head head = new Head(7,7);
    public  ArrayList<Tail> tails = new ArrayList<>();

    public AI snakeAI = new AI(this);

    public Snake(int id)
    {
        ID = id;
    }

    public void addTail()
    {
        int lastX = tails.isEmpty() ? head.getX() : tails.get(tails.size()-1).getX();
        int lastY = tails.isEmpty() ? head.getY() : tails.get(tails.size()-1).getY();

        switch(head.getDir()) {
            case RIGHT:
                lastX -= 1;
                break;
            case UP:
                lastY += 1;
                break;
            case LEFT:
                lastX += 1;
                break;
            case DOWN:
                lastY -= 1;
                break;
        }

        tails.add(new Tail(lastX,lastY));
    }

    public void move(){

        if(aiContrrolled)
        {
            snakeAI.initGrid(this);
            AI.Pair move = snakeAI.aiMove(snakeAI.AStar());

            if(move.x == 1)
                head.setDir(RIGHT);
            else if( move.x == -1)
                head.setDir(LEFT);
            else if(move.y == 1)
                head.setDir(DOWN);
            else if(move.y == -1)
                head.setDir(UP);
        }


        //Move Tails
        if(tails.size() >=2){
            for(int i = tails.size()-1; i>=1;  i--){
                if(tails.get(i).isWait()){
                    tails.get(i).setWait(false);
                }else {
                    tails.get(i).setX(tails.get(i-1).getX());
                    tails.get(i).setY(tails.get(i-1).getY());
                }
            }
        }
        //Move first Tal to Head
        if(tails.size()>=1){
            if(tails.get(0).isWait()){
                tails.get(0).setWait(false);
            }else {
                tails.get(0).setX(head.getX());
                tails.get(0).setY(head.getY());
            }
        }
        //Move Head
        switch(head.getDir()){
            case RIGHT:
                head.setX(head.getX() + 1);
                break;
            case UP:
                head.setY(head.getY() - 1);
                break;
            case LEFT:
                head.setX(head.getX() - 1);
                break;
            case DOWN:
                head.setY(head.getY() + 1);
                break;

        }
    }

    public  boolean collideWall(){
        return (head.getX() < 0 || head.getX() > 15 || head.getY() <0 || head.getY() > 15);

    }

    //TODO own score

    public  void collidePickUp(){
        if(head.getX()== GameClock.pickup.getX() && head.getY() == GameClock.pickup.getY()){
            addTail();
            Scoreboard.scores.set(ID,Scoreboard.scores.get(ID)+1);
            if(Scoreboard.scores.get(ID) > Scoreboard.bestscore) Scoreboard.bestscore = Scoreboard.scores.get(ID);

            GameClock.createPickup();
        }
    }

    public boolean collideSnake(Snake anotherSnake)
    {
        for(int i = 0; i < anotherSnake.tails.size(); i++) {
            if(anotherSnake.tails.get(i).collision(head.getX(),head.getY())) {
                return true;
            }
        }

        if(anotherSnake.head.collision(head.getX(),head.getY()) && anotherSnake != this)
            return true;

        return false;
    }

    //Position to Coordinates
    public static Point ptc(int x, int y)
    {
        Point p =  new Point(0,0);
        p.x = x * 32 + Gui.xoff;
        p.y = y * 32 + Gui.yoff;

        return p;
    }
}
