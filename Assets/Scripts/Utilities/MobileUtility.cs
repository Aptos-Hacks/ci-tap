using UnityEngine;

public delegate void OnTouch(Vector3 touchPosition);
public static class MobileUtility
{
    public class AddMobileRequiredComponentsOptions
    {
        public bool Touch { get; set; } = true;
    }

    public static void HandleTouch(Transform transform, OnTouch OnTouch)
    {
        if (Input.touchCount == 1)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                var collider = transform.GetComponent<Collider2D>();
                if (collider.bounds.Contains(touch.position))
                {
                    OnTouch(touch.position);
                }
            }

        }
    }
}
