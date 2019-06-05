package game;

import clocks.GameClock;

import java.util.concurrent.ThreadLocalRandom;

public class PickUp extends Collidable
{
    public PickUp(){
        super(ThreadLocalRandom.current().nextInt(0,15),
                ThreadLocalRandom.current().nextInt(0,15));

        boolean collided;

        while(checkIfCollided())
        {
            setX(ThreadLocalRandom.current().nextInt(0, 15));
            setY(ThreadLocalRandom.current().nextInt(0, 15));
        }
    }

    public void reset(){
        this.x = ThreadLocalRandom.current().nextInt(0,15);
        this.y = ThreadLocalRandom.current().nextInt(0,15);
    }

    public boolean checkIfCollided()
    {
        boolean collided = false;
        for (Snake snake : GameClock.snakes) {
            for (Tail tail : snake.tails) {
                if (tail.collision(getX(), getY()))
                    collided = true;
            }

            if (snake.head.collision(getX(), getY()))
                collided = true;
        }

        if (GameClock.obstacle.collision(getX(), getY()))
            collided = true;

        return collided;
    }

    public void action()
    {
        return;
    }
    public void onEaten() {return; }
}
