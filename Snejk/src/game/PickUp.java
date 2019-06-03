package game;

import java.util.concurrent.ThreadLocalRandom;

public class PickUp extends Collidable
{
    public PickUp(){
        super(ThreadLocalRandom.current().nextInt(0,15),
                ThreadLocalRandom.current().nextInt(0,15));
    }

    public void reset(){
        this.x = ThreadLocalRandom.current().nextInt(0,15);
        this.y = ThreadLocalRandom.current().nextInt(0,15);
    }

    public void action()
    {
        return;
    }
}
