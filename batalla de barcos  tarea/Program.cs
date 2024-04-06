static void JuegoDeGuerra(int[,] mapaEnemigo, int[] tamanosNaves, string[] nombresNaves)
{
    ColocarNaves(mapaEnemigo, tamanosNaves, nombresNaves);
    int contadorDisparos = 0;
    int aciertos = 20;
    int puntos = 8000;

    do
    {
        Console.WriteLine("enemigos que estan en el oseano:");
        for (int i = 0; i < nombresNaves.Length; i++)
        {
            Console.WriteLine("{0}: {1}", nombresNaves[i], tamanosNaves[i]);
        }
        Console.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
        Console.WriteLine("Debe acertar un total de {0} disparos", aciertos);
        Console.WriteLine("Cañonasos: " + contadorDisparos);
        Console.WriteLine("Cañonaso dado: " + (20 - aciertos));
        Console.WriteLine("Puntaje: " + puntos);
        Console.WriteLine();
        MostrarMapa(mapaEnemigo);
        Console.WriteLine();

        try
        {
            Console.WriteLine("\nElija la posición a atacar (fila, columna)\n");
            Console.Write("> ");
            //colocar una coma para la posicion y no tener que dar dos enter para colocar otro numero
            string[] posicion = Console.ReadLine().Split(',');
            int fila = int.Parse(posicion[0]) - 1;
            int columna = int.Parse(posicion[1]) - 1;
            //verificar si el la posicion dicha es correcta y le dio a un barco
            if (mapaEnemigo[fila, columna] == 1 || mapaEnemigo[fila, columna] == 2 || mapaEnemigo[fila, columna] == 3)
            {
                mapaEnemigo[fila, columna] = 7;
                Console.Beep();
                Console.Clear();
                Console.WriteLine("¡Acertaste Capitán! +150pts\n");
                puntos += 150;
                aciertos--;
                contadorDisparos++;
            }
            //verfica si la posicion no dio en un barco 
            else if (mapaEnemigo[fila, columna] == 0)
            {
                mapaEnemigo[fila, columna] = 5;
                Console.Clear();
                Console.WriteLine("Fallaste marinero de agua dulce -250pts\n");
                puntos -= 250;
                contadorDisparos++;
            }
            //verificar si ya le dio al mismo lugar 
            else if (mapaEnemigo[fila, columna] == 7 || mapaEnemigo[fila, columna] == 5)
            {
                Console.Clear();
                Console.WriteLine("¡Ya habías disparado a esa posición -750pts");
                Console.WriteLine("no te vuelvas a equivocar sino ya sabes que pasa :3");
                puntos -= 750;
                contadorDisparos++;
            }
        }
        catch (Exception)
        {
            Console.Clear();
            Console.WriteLine("La ubicación seleccionada no es válida\n");
            Console.WriteLine("Presione \"ENTER\" para continuar");
            Console.ReadKey();
        }
        //si todos los puntos y disparos se terminan terminar el juego 
    } while (aciertos > 0 && puntos > 0);
    Console.WriteLine();
     Console.WriteLine("presione enter para regresar al menu principal");
Console.ReadKey();
     
}
static void MostrarMapa(int[,] mapa)
{
    Console.Write("   ");
    for (int col = 1; col <= mapa.GetLength(1); col++)
    {
        Console.Write($"{col,3}");
    }
    Console.WriteLine();
    Console.WriteLine();

    for (int fila = 1; fila <= mapa.GetLength(0); fila++)
    {
        Console.Write($"{fila,3}");
        for (int col = 1; col <= mapa.GetLength(1); col++)
        {
            if (mapa[fila - 1, col - 1] == 0)
            {
                Console.Write(string.Format("{0,3}", "-"));
            }
            else if (mapa[fila - 1, col - 1] < 4)
            {
                Console.Write(string.Format("{0,3}", "-"));
            }
            else if (mapa[fila - 1, col - 1] == 7)
            {
                Console.Write(string.Format("{0,3}", "X"));
            }
            else if (mapa[fila - 1, col - 1] == 5)
            {
                Console.Write(string.Format("{0,3}", "O"));
            }
        }
        Console.WriteLine();
    }
}

static void ColocarNaves(int[,] mapa, int[] tamanosNaves, string[] nombresNaves)
{
    // generador de numeros aleatorios
    Random aleatorio = new Random();

    // Inicializar el mapa con valores vacíos
    for (int x = 0; x < mapa.GetLength(0); x++)
    {
        for (int y = 0; y < mapa.GetLength(1); y++)
        {
            mapa[x, y] = 0;
        }
    }

    // Para cada tipo de nave
    for (int i = 0; i < tamanosNaves.Length; i++)
    {
        int cantidadNaves = tamanosNaves[i];

        // Generar la cantidad indicada de naves
        for (int j = 0; j < cantidadNaves; j++)
        {
            int filaInicial, columnaInicial, direccion;
            bool posicionValida;
            //coloca la posicion de la nave aleatoria mente 
            do
            {
                posicionValida = true;
                filaInicial = aleatorio.Next(0, mapa.GetLength(0));
                columnaInicial = aleatorio.Next(0, mapa.GetLength(1));
                direccion = aleatorio.Next(2);
                //verificar si  la posicion de la nave a colocar 
                if ((direccion == 0 && columnaInicial + tamanosNaves[i] > mapa.GetLength(1)) ||
                    (direccion == 1 && filaInicial + tamanosNaves[i] > mapa.GetLength(0)))
                {
                    posicionValida = false;
                    continue;
                }
                //verificar si ya hay una nave en ese espacio generar otro lugar para colocar 
                for (int k = 0; k < tamanosNaves[i]; k++)
                {
                    if (direccion == 0 && mapa[filaInicial, columnaInicial + k] != 0 ||
                        direccion == 1 && mapa[filaInicial + k, columnaInicial] != 0)
                    {
                        posicionValida = false;
                        break;
                    }
                }
            } while (!posicionValida);

            // Colocar la nave en el mapa
            for (int k = 0; k < tamanosNaves[i]; k++)
            {
                if (direccion == 0)
                {
                    mapa[filaInicial, columnaInicial + k] = i + 1;
                }
                else
                {
                    mapa[filaInicial + k, columnaInicial] = i + 1;
                }
            }
        }
    }
}


int opcion = 3;


do
{
    try
    {
        Console.WriteLine("-----------------------------------------------------------------------------------------------------");
        Console.WriteLine("bienvenido al juego de barco");
        Console.WriteLine();
        Console.WriteLine(" opcion1 (1) jugar ");
        Console.WriteLine();
        Console.WriteLine(" opcion2 (3) salir");
        opcion = Convert.ToInt32(Console.ReadLine());

        switch (opcion)
        {
            case 1:
                int[,] mapaEnemigo = new int[20, 20];
                int[] tamanosNaves = { 2, 3, 4 };
                string[] nombresNaves = { "Barco", "Navío", "Portaaviones" };
                JuegoDeGuerra(mapaEnemigo, tamanosNaves, nombresNaves);
                break;


        }
    }
    catch (Exception)
    {
        Console.Clear();
        Console.WriteLine("tecla invalida \n");
        Console.WriteLine("Presione \"ENTER\" para continuar");
        Console.ReadKey();

    }

}
while (opcion != 3);
{
    Console.WriteLine("gracias por jugar nos vemos en la otra ");
}
