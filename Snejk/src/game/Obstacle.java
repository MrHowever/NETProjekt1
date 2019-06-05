package game;

import java.util.ArrayList;

public class Obstacle extends Collidable
{
    public ArrayList<Tail> segments = new ArrayList<>();
    public int size;
    public Dir direction;

    public float lifetime = 5000000000f;      //5 seconds in nanoseconds
    public long bornAt = 0;
    public boolean alive = false;

    public Obstacle()
    {
        super((int) (Math.round(Math.random() * 15)),(int) (Math.round(Math.random() * 15)));
        bornAt = System.nanoTime();
        alive = true;
        size = (int) (Math.round((Math.random() * 4)+1));
        direction = Dir.values()[(int) Math.round(Math.random() * 3)];

        int x = getX();
        int y = getY();

        segments.add(new Tail(x,y));
    }

    public boolean collision(int x1, int y1)
    {
        for(Tail segment : segments)
        {
            if(segment.getX() == x1 && segment.getY() == y1)
                return true;
        }

        return false;
    }

    public void action(){}
}
