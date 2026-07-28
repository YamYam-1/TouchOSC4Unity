# TouchOSC4Unity

TouchOSC와 OSC(Open Sound Control)를 이용해
모바일에서 Unity 조명을 실시간으로 제어하는 프로젝트입니다.
OSC 메시지를 수신하여 조명의 색상, 밝기 등의 값을 즉시 반영합니다.

---

## Demo

<img width="2560" height="1440" alt="Image" src="https://github.com/user-attachments/assets/2c5fca5b-5244-4b5e-84be-aba34f5b9afa" />

---

## Tech Stack

- Universal Render Pipeline (URP)
- HLSL

---

## Features

TouchOSC에서 전송한 OSC(Open Sound Control) 메시지를 Unity에서 수신하여 공연 조명을 실시간으로 제어합니다.
OSCManager는 주소(Address)에 따라 각 그룹의 조명 설정을 변경하고, PerObjectColor는 현재 모드를 기반으로 색상과 애니메이션을 적용합니다.
```cpp
_osc.SetAddressHandler($"/bpm/{index+1}", msg => groups[index].SetBpm(msg.GetFloat(0)));
_osc.SetAddressHandler($"/fade/{index+1}", msg => groups[index].SetFade(msg.GetFloat(0)));
_osc.SetAddressHandler($"/rmode/{index+1}", msg => groups[index].SetRMode(msg.GetInt(0)));
_osc.SetAddressHandler($"/cmode/{index+1}", msg => groups[index].SetCMode(msg.GetInt(0)));
```
TouchOSC에서 전송한 OSC Address를 등록하여 BPM, 밝기(Fade), 회전 모드, 색상 모드를 실시간으로 변경합니다.

수신된 데이터는 현재 선택된 Lighting Mode를 통해 각 조명의 상태를 계산합니다.

```cpp
LightingMode.RotationMode rm = LightingMode.RGetMode(rmode);
LightingMode.ColorMode cm = LightingMode.CGetMode(cmode);

rm(beat, targets[i], i);
LightingMode.LightState state = cm(beat, fade, color_A, color_B, i);
```
Delegate 기반으로 Rotation Mode와 Color Mode를 분리하여 새로운 조명 패턴을 쉽게 추가할 수 있도록 설계했습니다.


### 
---

## Refrence

Efficient GPU Screen-Space Ray Tracing - Morgan McGuire
