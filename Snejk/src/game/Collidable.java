package game;

public class Collidable extends Drawable
{
    public Collidable(int x1, int y1)
    {
        super(x1,y1);
    }

    public boolean collision(int x1, int y1)
    {
        return getX() == x1 && getY() == y1;
    }
}
