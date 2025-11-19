using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Oversteer : MonoBehaviour
    {
        [field: Header("Parameter")]
        [field: Tooltip("Amount value of oversteer")]
        [field: SerializeField] public float Amount { get; private set; }
        private Rigidbody2D rb;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Applied();
        }

        private void Applied()
        {
            Vector2 forward = transform.up * Vector2.Dot(rb.linearVelocity, transform.up);
            Vector2 rightward = transform.right * Vector2.Dot(rb.linearVelocity, transform.right);

            rb.linearVelocity = forward + Amount * Time.deltaTime * rightward;
        }
    }
}