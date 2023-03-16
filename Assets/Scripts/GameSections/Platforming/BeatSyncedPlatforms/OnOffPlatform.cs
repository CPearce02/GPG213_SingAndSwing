using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffPlatform : MonoBehaviour
{
    [SerializeField] Color onColor = Color.white, offColor = Color.grey;
    Collider2D _coll;
    SpriteRenderer _spriteRenderer;
    [SerializeField] bool inverted;
    bool _toggled = false;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _coll = GetComponent<Collider2D>();

        if (inverted) TogglePlatform();
    }

    public void TogglePlatform()
    {
        //Toggle off
        if(_toggled)
        {
            DisablePlatform();
            _toggled = false;
            return;
        }

        //Toggle on
        if (!_toggled)
        {
            EnablePlatform();
            _toggled = true;
            return;
        }
    }

    void EnablePlatform()
    {
        _coll.enabled = true;
        _spriteRenderer.color = onColor;
    }

    void DisablePlatform()
    {
        _coll.enabled = false;
        _spriteRenderer.color = offColor;
    }
}
