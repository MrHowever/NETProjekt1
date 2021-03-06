package game;

import clocks.GameClock;

import java.util.ArrayList;
import java.util.LinkedList;
import java.util.List;
import java.util.Stack;

public class AI
{
    public int[][] grid = new int[16][16];
    public int destinationX, destinationY;
    public int startX, startY;
    public final int dimensions = 16;

    public class Cell
    {
        int parentX, parentY;
        double f,g,h;
    }

    public class Pair
    {
        int x, y;

        public Pair(int x1, int y1)
        {
            x = x1;
            y = y1;
        }
    }

    public class Node
    {
        double f;
        Pair coords;

        public Node(double f1, Pair coords1)
        {
            f = f1;
            coords = coords1;
        }
    }

    public AI(Snake snake)
    {
    }

    public void initGrid(Snake snake)
    {
        for(int i = 0; i < dimensions; i++)
        {
            for(int j = 0; j < dimensions; j++)
            {
                grid[i][j] = 1;
            }
        }

        //0 means the cell is blocked by an obstacle
        for(int i = 0; i < GameClock.obstacle.segments.size(); i++)
        {
            grid[GameClock.obstacle.segments.get(i).getX()][GameClock.obstacle.segments.get(i).getY()] = 0;
        }

        for(Snake snakerino : GameClock.snakes)
        {
            for(int i = 0; i < snakerino.tails.size(); i++) {
                grid[snakerino.tails.get(i).getX()][snakerino.tails.get(i).getY()] = 0;
            }

            if(snakerino != snake)
                grid[snakerino.head.getX()][snakerino.head.getY()] = 0;
        }

        destinationX = GameClock.pickup.getX();
        destinationY = GameClock.pickup.getY();

        startX = snake.head.getX();
        startY = snake.head.getY();
    }

    boolean isValid(int x, int y)
    {
        if( x > 15 || x < 0 || y > 15 || y < 0)
            return false;

        return true;
    }

    boolean isUnBlocked(int x, int y)
    {
        if(grid[x][y] == 1)
            return true;

        return false;
    }

    boolean isDestination(int x, int y)
    {
        return x == destinationX && y == destinationY;
    }

    //Manhattan metric
    double HValue(int x, int y)
    {
        return Math.abs(x - destinationX) + Math.abs(y - destinationY);
    }

    Pair tracePath(Cell[][] cells)
    {
        int row = destinationY,col = destinationX;

        Stack<Pair> path = new Stack<>();

        while(!(cells[col][row].parentX == col && cells[col][row].parentY == row))
        {

            path.push(new Pair(col,row));

            int wtf = cells[col][row].parentX;
            int wtf2 = cells[col][row].parentY;

            row = wtf2;
            col = wtf;
        }

        Pair valRet =  path.peek();

        path.push(new Pair(col,row));


        while(!path.empty())
        {
            Pair tempPair = path.peek();
            path.pop();
        }

        return valRet;
    }

    public Pair aiMove(Pair position)
    {
        return new Pair(position.x - startX,position.y - startY);
    }

    public Pair AStar()
    {

        if(!isValid(startX,startY) || !isValid(destinationX,destinationY))
        {
            System.out.println("Incorrect input");
            return new Pair(-1,-1);
        }

        if(isUnBlocked(startX,startY) == false || isUnBlocked(destinationX,destinationY) == false)
        {
            System.out.println("No path available");

            if(isValid(startX+1,startY) && isUnBlocked(startX+1,startY))
                return new Pair(startX+1,startY);
            if(isValid(startX-1,startY) && isUnBlocked(startX-1,startY))
                return new Pair(startX-1,startY);
            if(isValid(startX,startY+1) && isUnBlocked(startX,startY+1))
                return new Pair(startX,startY+1);
            if(isValid(startX,startY-1) && isUnBlocked(startX,startY-1))
                return new Pair(startX,startY-1);
        }

        if(startX == destinationX && startY == destinationY)
        {
            System.out.println("Already at destination");
            return new Pair(-1,-1);
        }

        boolean[][] closedList = new boolean[dimensions][dimensions];

        for(int i = 0; i < dimensions; i++) {
            for (int j = 0; j < dimensions; j++) {
                closedList[i][j] = false;
            }
        }

        Cell[][] cellDetails = new Cell[dimensions][dimensions];

        for(int i = 0; i < dimensions; i++) {
            for (int j = 0; j < dimensions; j++) {
                cellDetails[i][j] = new Cell();
                cellDetails[i][j].f = Float.MAX_VALUE;
                cellDetails[i][j].g = Float.MAX_VALUE;
                cellDetails[i][j].h = Float.MAX_VALUE;
                cellDetails[i][j].parentX = -1;
                cellDetails[i][j].parentY = -1;
            }
        }

        int x = startX;
        int y = startY;

        cellDetails[x][y].f = 0.0;
        cellDetails[x][y].g = 0.0;
        cellDetails[x][y].h = 0.0;
        cellDetails[x][y].parentX = x;
        cellDetails[x][y].parentY = y;

        List<Node> openList = new LinkedList<>();
        openList.add(new Node(0.0,new Pair(x,y)));

        boolean destinationReached = false;

        while(!openList.isEmpty())
        {
            Node node = openList.get(0);
            openList.remove(0);

            x = node.coords.x;
            y = node.coords.y;
            closedList[x][y] = true;

            double fNew, gNew, hNew;

            int[] xCombinations = new int[]{
                                            x-1,
                                            x+1,
                                            x,
                                            x};

            int[] yCombinations = new int[]{
                                            y,
                                            y,
                                            y+1,
                                            y-1};

            int tempX, tempY;

            for(int i = 0; i < 4; i++) {
                tempX = xCombinations[i];
                tempY = yCombinations[i];

                if (isValid(tempX,tempY)) {
                    if (isDestination(tempX,tempY)) {
                        cellDetails[tempX][tempY].parentX = x;
                        cellDetails[tempX][tempY].parentY = y;
                         destinationReached = true;
                        return tracePath(cellDetails);
                    } else if (closedList[tempX][tempY] == false && isUnBlocked(tempX,tempY) == true) {
                        gNew = cellDetails[x][y].g + 1.0;
                        hNew = HValue(tempX,tempY);
                        fNew = gNew + hNew;

                        if (cellDetails[tempX][tempY].f == Float.MAX_VALUE || cellDetails[tempX][tempY].f > fNew) {
                            openList.add(new Node(fNew, new Pair(tempX,tempY)));

                            cellDetails[tempX][tempY].f = fNew;
                            cellDetails[tempX][tempY].g = gNew;
                            cellDetails[tempX][tempY].h = hNew;
                            cellDetails[tempX][tempY].parentX = x;
                            cellDetails[tempX][tempY].parentY = y;
                        }
                    }
                }
            }
        }

        if(!destinationReached)
        {
            System.out.println("Failed to reach destination");
        }

        return new Pair(-1,-1);
    }
}
