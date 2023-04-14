static void MoveBulletLeft(ref int bulletLeftCount, int[] bulletLeftX, int[] bulletLeftY, bool[] isBulletLeftActive, char[,] maze)
        {
            for (int x = 0; x < bulletLeftCount; x++)
            {
                char next = maze[bulletLeftY[x], bulletLeftX[x] - 1]; 
                if (next != ' ')
                {
                    EraseLeftBullet(bulletLeftX[x], bulletLeftY[x]);
                    MakeBulletLeftInactive(ref x,  isBulletLeftActive);
                }
                else
                {
                    EraseLeftBullet(bulletLeftX[x], bulletLeftY[x]);
                    bulletLeftX[x] = bulletLeftX[x] - 1;
                    PrintLeftBullet(ref bulletLeftX[x],ref bulletLeftY[x]);
                }
            }
        }
static void MoveBullet(ref int bulletCount, int[] bulletX, int[] bulletY, bool[] isBulletActive, char[,] maze)
        {
            for (int x = 0; x < bulletCount; x++)
            {
                char next = maze[bulletY[x], bulletX[x] + 1];
                if (next != ' ')
                {
                    EraseBullet(bulletX[x], bulletY[x]);
                    MakeBulletInactive(ref x, isBulletActive);
                }
                else
                {
                    EraseBullet(bulletX[x], bulletY[x]);
                    bulletX[x] = bulletX[x] + 1;
                    PrintBullet(ref bulletX[x],ref bulletY[x]);
                }
            }
        } 
 static void MakeBulletLeftInactive(ref int index, bool[] isBulletLeftActive)
        {
            isBulletLeftActive[index] = false;
        }               
static void MakeBulletInactive(int index, bool[] isBulletActive)
        {
            isBulletActive[index] = false;
        }
