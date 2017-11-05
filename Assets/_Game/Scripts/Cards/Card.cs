using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private ValueCard value;

    public override bool Equals(object obj)
    {
        // Check for null values and compare run-time types.
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Card other = (Card) obj;
        return other.value == value;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return value.ToString();
    }
}
