using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atom_Boom : MonoBehaviour
{
    public Flame flame;
    public float spacing = 0;

    public ParticleSystem particleSystem;
    public AudioClip boom;
    public void HandleExplosion()
    {
        GameController.Instance.musicManager.PlayOneShot(boom);
        SpawnCross(2);
        SimplePool2.Despawn(this.gameObject);

    }
    void SpawnCross(int units)
    {
        // Vị trí trung tâm
        Vector3 centerPosition = this.transform.position;

        // Tạo phần tử ở trung tâm
        SimplePool2.Spawn(flame, centerPosition, Quaternion.identity);

        // Tạo phần tử mở rộng theo hướng trên, dưới, trái, phải
        for (int i = 1; i <= units; i++)
        {
            // Tạo phần tử phía trên
            Vector3 upPosition = centerPosition + new Vector3(0, spacing, 0) * i;
           var temp = SimplePool2.Spawn(particleSystem, upPosition, Quaternion.identity);
            temp.transform.localEulerAngles = new Vector3(90,0,0);
            temp.Play();
            SimplePool2.Spawn(flame, upPosition, Quaternion.identity);
    
            

            // Tạo phần tử phía dưới
            Vector3 downPosition = centerPosition + new Vector3(0, -spacing, 0) * i;
            var temp2 = SimplePool2.Spawn(particleSystem, downPosition, Quaternion.identity);
            temp2.transform.localEulerAngles = new Vector3(90, 0, 0);
            temp2.Play();
            SimplePool2.Spawn(flame, downPosition, Quaternion.identity);

            // Tạo phần tử phía trái
            Vector3 leftPosition = centerPosition + new Vector3(-spacing, 0, 0) * i;
            var temp3 = SimplePool2.Spawn(particleSystem, leftPosition, Quaternion.identity);
            temp3.transform.localEulerAngles = new Vector3(90, 0, 0);
            temp3.Play();
            SimplePool2.Spawn(flame, leftPosition, Quaternion.identity);

            //Tạo phần tử phía phải
            Vector3 rightPosition = centerPosition + new Vector3(spacing, 0, 0) * i;
            var temp4 = SimplePool2.Spawn(particleSystem, rightPosition, Quaternion.identity);
            temp4.transform.localEulerAngles = new Vector3(90, 0, 0);
            temp4.Play();
            SimplePool2.Spawn(flame, rightPosition, Quaternion.identity);

            Vector3 upLeftPosition = centerPosition + new Vector3(-spacing, spacing, 0) * i;
            var temp5 = SimplePool2.Spawn(particleSystem, upLeftPosition, Quaternion.identity);
            temp5.transform.localEulerAngles = new Vector3(90, 0, 0);
            temp5.Play();
            SimplePool2.Spawn(flame, upLeftPosition, Quaternion.identity);

            Vector3 upRightPosition = centerPosition + new Vector3(spacing, spacing, 0) * i;
            var temp6 = SimplePool2.Spawn(particleSystem, upRightPosition, Quaternion.identity);
            temp6.transform.localEulerAngles = new Vector3(90, 0, 0);
            temp6.Play();
            SimplePool2.Spawn(flame, upRightPosition, Quaternion.identity);

            Vector3 downLeftPosition = centerPosition + new Vector3(-spacing, -spacing, 0) * i;
            var temp7 = SimplePool2.Spawn(particleSystem, downLeftPosition, Quaternion.identity);
            temp7.transform.localEulerAngles = new Vector3(90, 0, 0);
            temp7.Play();
            SimplePool2.Spawn(flame, downLeftPosition, Quaternion.identity);

            Vector3 downRightPosition = centerPosition + new Vector3(spacing, -spacing, 0) * i;
            var temp8 = SimplePool2.Spawn(particleSystem, downRightPosition, Quaternion.identity);
            temp8.transform.localEulerAngles = new Vector3(90, 0, 0);
            temp8.Play();
            SimplePool2.Spawn(flame, downRightPosition, Quaternion.identity);
        }


    }

}
