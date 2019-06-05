package game;

public class StaticObstacle extends Obstacle
{
    public StaticObstacle()
    {
        super();

        int direction = (int) Math.round(Math.random()*3);
        int dirX = 0, dirY = 0;

        switch(direction)
        {
            case 0:
                dirX = 1;
                break;
            case 1:
                dirX = -1;
                break;
            case 2:
                dirY = 1;
                break;
            case 3:
                dirY = -1;
                break;
        }

        segments.add(new Tail(getX(),getY()));

        if(direction == 0 || direction == 1) {
            while(segments.get(segments.size() - 1).getX() > 2 && segments.get(segments.size() - 1).getX() < 14)
                segments.add(new Tail(segments.get(segments.size()-1).getX() + dirX,segments.get(segments.size()-1).getY()));
        }

        if(direction == 2 || direction == 3) {
            while(segments.get(segments.size() - 1).getY() > 2 && segments.get(segments.size() - 1).getY() < 14)
                segments.add(new Tail(segments.get(segments.size() - 1).getX(), segments.get(segments.size() - 1).getY() + dirY));
        }
    }

    public void action()
    {
        if((System.nanoTime() - bornAt) > lifetime) {
            alive = false;
            segments.clear();
        }
    }
}
