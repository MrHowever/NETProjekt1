package game;

public class Frog extends PickUp
{
    boolean jump = true;

    public Frog()
    {
        super();
    }

    public void action()
    {
        if(jump) {
            int direction = (int) Math.round(Math.random());

            int move = (int) Math.round(Math.random());

            if (move == 0)
                move = -1;

            if (direction == 1) {
                if (getX() == 15)
                    move = -1;
                else if (getX() == 0)
                    move = 1;

                setX(getX() + move);
            } else if (direction == 0) {
                if (getY() == 15)
                    move = -1;
                else if (getY() == 0)
                    move = 1;

                setY(getY() + move);
            }

            jump = false;
        }
        else
            jump = true;
    }
}
