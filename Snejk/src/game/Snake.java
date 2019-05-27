package game;

import GUI.Gui;

import java.awt.*;
import java.util.ArrayList;

public class Snake
{
    public static boolean waitToMove = false;

    public static Head head = new Head(7,7);
    public static ArrayList<Tail> tails = new ArrayList<>();

    public static PickUp pickup = new PickUp();

    public static void addTail()
    {
        if(tails.size() < 1)
        {
            tails.add(new Tail(head.getX(), head.getY()));
        }else
        {
            tails.add(new Tail(tails.get(tails.size()-1).x, tails.get(tails.size()-1).y));
        }
    }

    public static void move(){
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

    //Position to Coordinates
    public static Point ptc(int x, int y)
    {
    Point p =  new Point(0,0);
    p.x = x * 32 + Gui.xoff;
    p.y = y * 32 + Gui.yoff;

    return p;
    }
}
