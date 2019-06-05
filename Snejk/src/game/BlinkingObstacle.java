package game;

import java.util.ArrayList;

public class BlinkingObstacle extends StaticObstacle
{
    public ArrayList<Tail> tempStorage = new ArrayList<>();
    int counter = 0;

    public void action()
    {
        counter++;

        if(counter == 3) {
            tempStorage = new ArrayList<>(segments);
            segments.clear();
        }

        if(counter  == 6 ) {
            segments = new ArrayList<>(tempStorage);
            tempStorage.clear();
            counter = 0;
        }

        if((System.nanoTime() - bornAt) > lifetime) {
            alive = false;
            segments.clear();
        }
    }
}
