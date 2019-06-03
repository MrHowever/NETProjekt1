package game;

public class Head extends Collidable
{
    Dir dir = Dir.RIGHT;

    public Head(int x, int y) {
        super(x,y);
    }

    public Dir getDir() {
        return dir;
    }

    public void setDir(Dir dir) {
        this.dir = dir;
    }
}
