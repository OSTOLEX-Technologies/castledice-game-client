using castledice_game_logic.Math;

namespace Src.PVE.MoveSearchers.TraitsEvaluators
{
    public class DijkstraMinPathCostSearcher : IGraphPathMinCostSearcher
    {
        //Generated with ChatGPT
        public int FindMinCost(int[,] graph, Vector2Int start, Vector2Int end)
        {
            int startX = start.X;
            int startY = start.Y;
            int endX = end.X;
            int endY = end.Y;
            
            int rows = graph.GetLength(0);
            int cols = graph.GetLength(1);

            int[,] distance = new int[rows, cols];
            bool[,] visited = new bool[rows, cols];

            // Инициализируем расстояния как бесконечность, кроме стартовой точки
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    distance[i, j] = int.MaxValue;
                }
            }
            distance[startX, startY] = 0;

            // Функция для нахождения соседних вершин
            int[,] directions = {
                { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 },  // Основные направления
                { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 }  // Диагональные направления
            };

            // Основной цикл алгоритма
            for (int count = 0; count < rows * cols - 1; count++)
            {
                int minDistance = int.MaxValue;
                int minIndexX = -1, minIndexY = -1;

                // Находим непосещённую вершину с минимальным расстоянием
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

                // Помечаем выбранную вершину как посещённую
                visited[minIndexX, minIndexY] = true;

                // Обновляем расстояния до соседних вершин
                for (int k = 0; k < 8; k++)
                {
                    int nx = minIndexX + directions[k, 0];
                    int ny = minIndexY + directions[k, 1];
                    if (nx >= 0 && nx < rows && ny >= 0 && ny < cols && !visited[nx, ny] &&
                        distance[minIndexX, minIndexY] + graph[nx, ny] < distance[nx, ny])
                    {
                        distance[nx, ny] = distance[minIndexX, minIndexY] + graph[nx, ny];
                    }
                }
            }

            // Возвращаем значение расстояния до целевой точки
            return distance[endX, endY] - graph[endX, endY];
        }
    }
}