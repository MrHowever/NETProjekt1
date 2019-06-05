package game;

public class Tail extends Collidable
{
    boolean wait = false;

    public Tail(int x, int y) {
        super(x,y);
    }

    public boolean isWait() {
        return wait;
    }

    public void setWait(boolean wait) {
        this.wait = wait;
    }
}
