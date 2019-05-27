package game;

import java.util.ArrayList;

public class Snake
{
    public static boolean waitToMove = false;

    public static Head head = new Head(7,7);
    public static ArrayList<Tail> tails = new ArrayList<>();

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


    }

}
