using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    [System.Serializable] public struct SpriteAnimation
    {
        public string name;
        public List<Sprite> sprites;
        public float speed;
    }
    public List<SpriteAnimation> animations;
    private SpriteRenderer spriteRenderer;
    private string currentAnimation;

    public void PlayAnimation(string animationName)
    {
        if(currentAnimation == animationName)
        {
            return;
        }
        foreach (var animation in animations)
        {
            if(animation.name == animationName)
            {
                StopAllCoroutines();
                StartCoroutine(AnimationCoroutine(animation));
            }
            
        }
    }
    public IEnumerator AnimationCoroutine(SpriteAnimation animation)
    {
        currentAnimation = animation.name;
        int currentSpriteIdx = 0;
        while(true)
        {
            spriteRenderer.sprite = animation.sprites[currentSpriteIdx];
            currentSpriteIdx++;

            if(currentSpriteIdx == animation.sprites.Count)
                currentSpriteIdx = 0;

            yield return new WaitForSeconds(animation.speed);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
