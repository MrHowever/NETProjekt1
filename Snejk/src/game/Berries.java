package game;

public class Berries extends PickUp
{
    public Berries()
    {
        randomEdgePosition();
    }

    public void reset()
    {
        randomEdgePosition();
    }

    public void randomEdgePosition()
    {
        int edge = (int) Math.round(Math.random());
        int side = (int) Math.round(Math.random());
        int randomVal = (int) (Math.round(Math.random()*15));

        if(edge == 1) {   // Horizontal edge
            if(side == 1) {  // Right side
                setX(15);
                setY(randomVal);
            }
            else {          //Left side
                setX(0);
                setY(randomVal);
            }
        }
        else {          //Vertical edge
            if(side == 1) {  // Upper side
                setX(randomVal);
                setY(0);
            }
            else {          //Lower side
                setX(randomVal);
                setY(15);
            }
        }
    }
}
