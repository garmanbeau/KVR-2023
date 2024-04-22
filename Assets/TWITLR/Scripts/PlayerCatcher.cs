// 2024-02-03 AI-Tag 
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using UnityEngine;
using System.Text.RegularExpressions;

public class PlayerCatcher : MonoBehaviour
{
    public Transform[] transforms;

    private void OnCollisionEnter(Collision collision)
    {
        string name = collision.gameObject.name;
        string lastDigitStr = name.Substring(name.Length - 1);

        if (Regex.IsMatch(lastDigitStr, @"\d"))
        {
            Rigidbody otherRb = collision.gameObject.GetComponent<Rigidbody>();
            if (otherRb)
            {
                otherRb.velocity = Vector3.zero;
                otherRb.angularVelocity = Vector3.zero;


                int index = int.Parse(lastDigitStr);
                if (index < transforms.Length)
                {
                    Transform targetTransform = transforms[index];
                    // Use Rigidbody's methods instead of setting transform directly
                    otherRb.MovePosition(targetTransform.position);
                    otherRb.MoveRotation(targetTransform.rotation);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        string name = other.gameObject.name;
        string lastDigitStr = name.Substring(name.Length - 1);

        if (Regex.IsMatch(lastDigitStr, @"\d"))
        {
            Rigidbody otherRb = other.gameObject.GetComponent<Rigidbody>();
            if (otherRb)
            {
                otherRb.velocity = Vector3.zero;
                otherRb.angularVelocity = Vector3.zero;
                

                int index = int.Parse(lastDigitStr);
                if (index < transforms.Length)
                {
                    Transform targetTransform = transforms[index];
                    // Use Rigidbody's methods instead of setting transform directly
                    otherRb.MovePosition(targetTransform.position);
                    otherRb.MoveRotation(targetTransform.rotation);
                }
            }
        }
    }
}
