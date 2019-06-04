package game;

public class MovingObstacle extends Obstacle
{
    boolean leaving = false;
    public MovingObstacle()
    {
        super();
    }

    public void action()
    {
        int xOffset = 0, yOffset = 0;
        switch(direction)
        {
            case RIGHT:
                xOffset = 1;
                break;
            case LEFT:
                xOffset = -1;
                break;
            case UP:
                yOffset = 1;
                break;
            case DOWN:
                yOffset = -1;
                break;
        }

        for(Tail tail : segments)
        {
            tail.setX(tail.getX()+xOffset);
            tail.setY(tail.getY()+yOffset);
        }

        for(int i = 0; i < segments.size(); i++)
        {
            if(segments.get(i).getX() < 0 || segments.get(i).getX() > 15 ||
                    segments.get(i).getY() < 0 || segments.get(i).getY() > 15)
            {
                segments.remove(i);
                leaving = true;
                break;
            }
        }

        if(segments.size() < size && !leaving)
        {
            segments.add(new Tail(segments.get(segments.size()-1).getX() -xOffset,
                    segments.get(segments.size()-1).getY() - yOffset));
        }
    }
}
