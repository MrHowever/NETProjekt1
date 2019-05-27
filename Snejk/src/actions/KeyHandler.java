package actions;

import game.Dir;
import game.Snake;

import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;

public class KeyHandler implements KeyListener
{
    @Override
    public void keyTyped(KeyEvent e) {

    }

    @Override
    public void keyPressed(KeyEvent e) {
        switch(e.getKeyCode()){
            case KeyEvent.VK_W:
                if(!(Snake.head.getDir()== Dir.DOWN) && !Snake.waitToMove) {
                    Snake.head.setDir(Dir.UP);
                    Snake.waitToMove = true;
                }
                break;
            case KeyEvent.VK_A:
                if(!(Snake.head.getDir()== Dir.RIGHT) && !Snake.waitToMove) {
                    Snake.head.setDir(Dir.LEFT);
                    Snake.waitToMove = true;
                }
                break;
            case KeyEvent.VK_S:
                if(!(Snake.head.getDir()== Dir.UP) && !Snake.waitToMove) {
                    Snake.head.setDir(Dir.DOWN);
                    Snake.waitToMove = true;
                }
                break;
            case KeyEvent.VK_D:
                if(!(Snake.head.getDir()== Dir.LEFT) && !Snake.waitToMove) {
                    Snake.head.setDir(Dir.RIGHT);
                    Snake.waitToMove = true;
                }
                break;
        }
    }

    @Override
    public void keyReleased(KeyEvent e) {

    }
}
