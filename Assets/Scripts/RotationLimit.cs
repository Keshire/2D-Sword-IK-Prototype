using UnityEngine;
using System.Collections;

public abstract class RotationLimit : MonoBehaviour {

	/// The main axis of the rotation limit.
	public Vector3 axis = Vector3.forward;
    private bool initiated;
    private bool applicationQuit;
    private bool defaultLocalRotationSet;

    protected abstract Quaternion LimitRotation(Quaternion rotation);

    //The default local rotation of the gameobject. By default stored in Awake.
    [HideInInspector]
    public Quaternion defaultLocalRotation;

    //An arbitrary secondary axis that we get by simply switching the axes
    public Vector3 secondaryAxis { get { return new Vector3(axis.y, axis.z, axis.x); } }

    //Cross product of axis and secondaryAxis
    public Vector3 crossAxis { get { return Vector3.Cross(axis, secondaryAxis); } }


    /// Map the zero rotation point to the current rotation
    public void SetDefaultLocalRotation() {
		defaultLocalRotation = transform.localRotation;
		defaultLocalRotationSet = true;
	}

	/// Returns the limited local rotation.
	public Quaternion GetLimitedLocalRotation(Quaternion localRotation, out bool changed) {
		// Making sure the Rotation Limit is initiated
		if (!initiated) Awake ();
			
		// Subtracting defaultLocalRotation
		Quaternion rotation = Quaternion.Inverse(defaultLocalRotation) * localRotation;
			
		Quaternion limitedRotation = LimitRotation(rotation);
		changed = limitedRotation != rotation;

		if (!changed) return localRotation;

		// Apply defaultLocalRotation back on
		return defaultLocalRotation * limitedRotation;
	}
		
	//Apply the rotation limit to transform.localRotation. Returns true if the limit has changed the rotation.
	public bool Apply() {
		bool changed = false;

		transform.localRotation = GetLimitedLocalRotation(transform.localRotation, out changed);

		return changed;
	}

	//Disable this instance making sure it is initiated. Use this if you intend to manually control the updating of this Rotation Limit.
	public void Disable() {
		if (initiated) {
			enabled = false;
			return;
		}

		Awake();
		enabled = false;
	}
       
	void Awake() {
		// Store the local rotation to map the zero rotation point to the current rotation
		if (!defaultLocalRotationSet) SetDefaultLocalRotation();
				
		if (axis == Vector3.zero) Debug.LogError("Axis is Vector3.zero.");
		initiated = true;
	}

	void LateUpdate() {
        Apply();
    }

	//Limits rotation to a single degree of freedom (along axis)
	protected static Quaternion Limit1DOF(Quaternion rotation, Vector3 axis) {
		return Quaternion.FromToRotation(rotation * axis, axis) * rotation;
	}
}