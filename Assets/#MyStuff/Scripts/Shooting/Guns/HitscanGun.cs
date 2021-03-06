using System;
using Ludiq.PeekCore;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HitscanGun : MonoBehaviour, IGun
{
	[SerializeField] private GameObject[] effects;
	[SerializeField] private float fireRate;
	[SerializeField] private Transform sphereCastOrigin;
	[SerializeField] private float sphereCastRadius;
	[SerializeField] private LayerMask interactibleMask;
	[SerializeField] private Slider effectSlider;
	[SerializeField] private bool invertScrollDirection;

	private float timer;
	private int activeEffectIndex;

	private void OnValidate()
	{
		foreach (var effect in effects)
		{
			Debug.Assert(effect.GetComponent<IHitscanEffect>() != null,"The prefab you assigned has no component that implements IHitscanEffect." );
		}
	}

	public void PrimaryMouseButtonEnter()
	{
		timer = fireRate;
	}
	public void PrimaryMouseButtonAction()
	{
		timer += Time.deltaTime;
		if (!(timer >= fireRate)) return;
		timer = 0;
		Shoot();
	}
	public void PrimaryMouseButtonExit() { }

	public void SecondaryMouseButtonEnter() { }
	public void SecondaryMouseButtonAction() { }
	public void SecondaryMouseButtonExit() { }

	public void ScrollWheelAction(InputAction.CallbackContext _context)
	{
		Vector2 input = _context.ReadValue<Vector2>();
		int projectileCount = effects.Length - 1;

		if(_context.started)
		{
			int direction = Mathf.RoundToInt(input.y);
			direction = invertScrollDirection ? -direction : direction;

			if (activeEffectIndex < projectileCount && activeEffectIndex > 0)
			{
				activeEffectIndex += direction;
			}
			else if (activeEffectIndex == projectileCount)
			{
				if (direction > 0)
				{
					activeEffectIndex = 0;
				}
				else if (direction < 0)
				{
					activeEffectIndex += direction;
				}
			}
			else if (activeEffectIndex == 0)
			{
				if (direction < 0)
				{
					activeEffectIndex = projectileCount;
				}
				else if (direction > 0)
				{
					activeEffectIndex = 1;
				}
			}
		}

		effectSlider.value = (float)activeEffectIndex / (float)projectileCount;
	}

	private void Shoot()
	{
		if (Physics.SphereCast(sphereCastOrigin.position, sphereCastRadius, Camera.main.transform.forward, out var hit,
			Mathf.Infinity, interactibleMask, QueryTriggerInteraction.Ignore))
		{
			var hitTransform = hit.collider.transform;
			if (hitTransform.GetComponentInChildren<IHitscanEffect>() != null)
			{
				return;
			}
			var effect = Instantiate(effects[activeEffectIndex], hitTransform.position, Quaternion.identity, hitTransform).GetComponent<IHitscanEffect>();
			effect.Initialize();
		}
	}
}
