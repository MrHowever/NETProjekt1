package actions;

import clocks.GameClock;
import game.Dir;
import game.Scoreboard;
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
                if(!(GameClock.snakes.get(0).head.getDir()== Dir.DOWN) && !Snake.waitToMove) {
                    GameClock.snakes.get(0).head.setDir(Dir.UP);
                    Snake.waitToMove = true;
                }
                break;
            case KeyEvent.VK_A:
                if(!(GameClock.snakes.get(0).head.getDir()== Dir.RIGHT) && !Snake.waitToMove) {
                    GameClock.snakes.get(0).head.setDir(Dir.LEFT);
                    Snake.waitToMove = true;
                }
                break;
            case KeyEvent.VK_S:
                if(!(GameClock.snakes.get(0).head.getDir()== Dir.UP) && !Snake.waitToMove) {
                    GameClock.snakes.get(0).head.setDir(Dir.DOWN);
                    Snake.waitToMove = true;
                }
                break;
            case KeyEvent.VK_D:
                if(!(GameClock.snakes.get(0).head.getDir()== Dir.LEFT) && !Snake.waitToMove) {
                    GameClock.snakes.get(0).head.setDir(Dir.RIGHT);
                    Snake.waitToMove = true;
                }
                break;
            case KeyEvent.VK_RIGHT:
                if(GameClock.snakes.size() > 1) {
                    if (!(GameClock.snakes.get(1).head.getDir() == Dir.LEFT) && !Snake.waitToMove) {
                        GameClock.snakes.get(1).head.setDir(Dir.RIGHT);
                        Snake.waitToMove = true;
                    }
                }
                break;
            case KeyEvent.VK_UP:
                if(GameClock.snakes.size() > 1) {
                    if (!(GameClock.snakes.get(1).head.getDir() == Dir.DOWN) && !Snake.waitToMove) {
                        GameClock.snakes.get(1).head.setDir(Dir.UP);
                        Snake.waitToMove = true;
                    }
                }
                break;
            case KeyEvent.VK_LEFT:
                if(GameClock.snakes.size() > 1) {
                    if (!(GameClock.snakes.get(1).head.getDir() == Dir.RIGHT) && !Snake.waitToMove) {
                        GameClock.snakes.get(1).head.setDir(Dir.LEFT);
                        Snake.waitToMove = true;
                    }
                }
                break;
            case KeyEvent.VK_DOWN:
                if(GameClock.snakes.size() > 1) {
                    if (!(GameClock.snakes.get(1).head.getDir() == Dir.UP) && !Snake.waitToMove) {
                        GameClock.snakes.get(1).head.setDir(Dir.DOWN);
                        Snake.waitToMove = true;
                    }
                }
                break;

            case KeyEvent.VK_N:
                GameClock.snakes.add(new Snake(GameClock.snakes.size()));
                Scoreboard.scores.add(0);
                break;
            case KeyEvent.VK_SPACE:
                if(GameClock.sleepTime > 100)
                    GameClock.sleepTime = GameClock.sleepTime - 100;
                break;
            case KeyEvent.VK_BACK_SPACE:
                GameClock.sleepTime = GameClock.sleepTime + 100;
                break;
        }
    }

    @Override
    public void keyReleased(KeyEvent e) {

    }
}
