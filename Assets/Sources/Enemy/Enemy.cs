using UnityEngine;

internal sealed class Enemy : Entity
{
    protected override void DeathEvent(Entity attacker)
    {

    }

    public override void Spawn(Location location)
    {
        int countCubeX = (int)location.GetSizeLocation().x;
        int countCubeY = (int)location.GetSizeLocation().y;

        for (int iterY = 0; iterY < countCubeY; iterY++)
        {
            for (int iterX = 0; iterX < countCubeX; iterX++)
            {
                if (iterY == 0 || iterX == 0 || iterY == countCubeY - 1 || iterX == countCubeX - 1)
                    continue;

                if (location.GetLocation()[iterX, iterY] != null)
                    continue;

                int randSpawnPosX = Random.Range(1, countCubeX - 2);
                int randSpawnPosZ = Random.Range(1, countCubeY - 2);

                Transform locTransf = location.GetPlaneLocation().transform;
                float lenPlane = location.GetLengthPlane();
                float spacing = location.GetSpacing();

                float posX = ((locTransf.position.x + 1) * iterX * randSpawnPosX) - lenPlane - 1.25f + (spacing * iterX);
                float posY = locTransf.position.y;
                float posZ = (locTransf.position.z * lenPlane - iterY * randSpawnPosZ) - (spacing * iterY);

                transform.position = new Vector3(posX, posY, posZ);
                return;
            }
        }
    }
}