using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Scripts.Player.Manager;
using Scripts.PlayerStateMachine;

namespace Scripts.Player
{
   public class Player : MonoBehaviour
   {
      #region States

      public StateMachine StateMachine { get; private set; }
      public StandingState StandingState { get; private set; }
      public JumpState JumpState { get; private set; }
      public LandingState LandingState { get; private set; }
      public SprintState SprintState { get; private set; }
      public AttackToolsState AttackToolsState { get; private set; }
      public AttackWeaponState AttackWeaponState { get; private set; }

      #endregion

      #region Controls Variables

      [Header("Controls")] public float moveSpeed = 5f;
      public float sprintSpeed = 8f;
      public float jumpHeight = 5f;
      public float gravityMultiplier = 2;

      #endregion

      #region Animation Smoothing

      [Header("Animation Smoothing")] [Range(0, 1)]
      public float speedDampTime = 0.1f;

      [Range(0, 1)] public float velocityDampTime = 0.9f;
      [Range(0, 1)] public float rotationDampTime = 0.2f;
      [Range(0, 1)] public float airControl = 0.5f;

      #endregion

      #region Components

      [HideInInspector] public CharacterController controller;
      [HideInInspector] public Animator animator;
      [HideInInspector] public Transform cameraTransform;
      [HideInInspector] public PlayerManager playerManager;

      public AnimatorOverrideController originalController;
      public AnimatorOverrideController overrideController;

      #endregion

      #region Other Variables

      [HideInInspector] public float gravityValue = -9.81f;
      [HideInInspector] public Vector3 playerVelocity;

      #endregion

      #region Ground Check Variables

      [Header("Ground Check Variables")] [SerializeField]
      private Transform groundCheckPosition;

      [SerializeField] private float groundCheckRadius = 0.2f;
      [SerializeField] private LayerMask groundCheckLayerMask;

      #endregion

      private void Awake()
      {
         controller = GetComponent<CharacterController>();
         animator = GetComponentInChildren<Animator>();
         cameraTransform = Camera.main.transform;
         playerManager = GetComponent<PlayerManager>();

         StateMachine = new StateMachine();
         StandingState = new StandingState(StateMachine, this, "Move");
         JumpState = new JumpState(StateMachine, this, "Jump");
         LandingState = new LandingState(StateMachine, this, "Landing");
         SprintState = new SprintState(StateMachine, this, "Move");
         AttackToolsState = new AttackToolsState(StateMachine, this, "AttackTools");
         AttackWeaponState = new AttackWeaponState(StateMachine, this, "AttackWeapon");
      }

      private void Start()
      {
         StateMachine.Initialize(StandingState);

         gravityValue *= gravityMultiplier;
      }

      private void Update()
      {
         StateMachine.CurrentState.LogicUpdate();
      }

      private void FixedUpdate()
      {
         StateMachine.CurrentState.PhysicsUpdate();
      }

      public bool IsGrounded()
      {
         return Physics.CheckSphere(groundCheckPosition.position, groundCheckRadius, groundCheckLayerMask);
      }

      public void AnimationFinishTrigger()
      {
         StateMachine.CurrentState.AnimationFinishTrigger();
      }

      public void AnimationTrigger()
      {
         StateMachine.CurrentState.AnimationTrigger();
      }
   }
}

