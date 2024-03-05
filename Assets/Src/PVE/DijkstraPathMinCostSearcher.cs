using castledice_game_logic.Math;

namespace Src.PVE
{
    public class DijkstraPathMinCostSearcher : IMatrixPathMinCostSearcher
    {
        public int GetMinCost(int[,] matrix, Vector2Int from, Vector2Int to)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            int[,] distance = new int[rows, cols];
            bool[,] visited = new bool[rows, cols];
            
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    distance[i, j] = int.MaxValue;
                }
            }
            distance[from.X, from.Y] = 0;
            
            int[,] directions = {
                { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 },  
                { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 }  
            };

            for (int count = 0; count < rows * cols - 1; count++)
            {
                int minDistance = int.MaxValue;
                int minIndexX = -1, minIndexY = -1;

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        if (!visited[i, j] && distance[i, j] < minDistance)
                        {
                            minDistance = distance[i, j];
                            minIndexX = i;
                            minIndexY = j;
                        }
                    }
                }

                visited[minIndexX, minIndexY] = true;

                for (int k = 0; k < 8; k++)
                {
                    int nextX = minIndexX + directions[k, 0];
                    int nextY = minIndexY + directions[k, 1];
                    if (nextX >= 0 && nextX < rows && nextY >= 0 && nextY < cols && !visited[nextX, nextY] &&
                        distance[minIndexX, minIndexY] + matrix[nextX, nextY] < distance[nextX, nextY])
                    {
                        distance[nextX, nextY] = distance[minIndexX, minIndexY] + matrix[nextX, nextY];
                    }
                }
            }

            return distance[to.X, to.Y];
        }
    }
}