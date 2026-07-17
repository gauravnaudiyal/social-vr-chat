# Social VR Chat

An XR project built in Unity for multiplayer communication across three themed virtual environments. Originally built for VRChat and later moved over to Meta Horizon Worlds. This was a module project for the XR course at Trinity College Dublin.

The interesting challenge here was not just building the environments but making them feel like places people would actually want to hang out in, rather than just empty rooms with avatars floating around.

## Scenes

- Office: interactable whiteboard with real-time sync, collaborative workspace feel
- Nature: a more relaxed outdoor environment for casual interaction
- Abstract/Art Space: a creative environment with a different vibe from the other two

## Features

- Multiplayer via Meta Horizon Worlds networking
- Spatial audio with proximity-based voice
- Avatar support
- Interactive in-world objects per scene

## Built with

- Unity (2022.3+)
- C#
- Meta Horizon Worlds SDK
- Meta XR SDK
- Blender and Mixamo for assets

## Getting started

```bash
git clone https://github.com/gauravnaudiyal/social-vr-chat.git
```

Open in Unity Hub, import the Meta XR All-in-One SDK via Package Manager, open a scene from `Scenes/` and hit Play. You will need a Meta Quest headset for the full experience.

Developed by Gaurav Naudiyal, MSc Computer Science, Trinity College Dublin.
