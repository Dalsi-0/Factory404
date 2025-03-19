
﻿ <div align="center">

## Factory404

<div align="center">
  <img src="이미지_주소" width="200"/>
</div>

<br/> [<img src="https://img.shields.io/badge/프로젝트 기간-2025.03.12~2025.03.19-73abf0?style=flat&logo=&logoColor=white" />]()

---
</div> 

## 📝 프로젝트 소개

퍼즐게임 기반에 추가로 공포 느낌을 더한 3D 게임 프로젝트입니다.
총 4개의 스테이지로 이루어져있으며, wasd(이동), shift(달리기), e(상호작용), f(손전등), tap(인벤토리) 입력키입니다.
또한 뭐뭐가 있다~

---

## 🎮 게임 기능 개요

| 기능 | 설명 |
|---|---|
| **🎯 이동 및 상호작용** | 이동 키만을 이용해 캐릭터를 조작할 수 있으며, 이동을 멈추면 자동으로 공격합니다. |
| **🏆 ** | 다양한 도전과제를 수행할 수 있습니다. |
| **🎲 스테이지 가이드라인** | 스테이지 클리어시마다 랜덤으로 주어지는 어빌리티를 조합하여 캐릭터를 성장시킵니다. |
| **🎮 ** | 적을 처치하고 점점 강력해지는 적들을 상대하며 스테이지를 클리어해야 합니다. |

---

## 📸 화면 구성
|메인 화면|
|:---:|
|<img src="https://github.com/Dalsi-0/LEGENOofOuch/blob/main/ReadMe/main.png?raw=true" width="700"/>|
|게임 시작 및 설정으로 이동할 수 있는 화면입니다.|

<br /><br />

|게임 플레이 장면|
|:---:|
|<img src="https://github.com/Dalsi-0/LEGENOofOuch/blob/main/ReadMe/option.png?raw=true" width="700"/>|
|키 바인딩, 사운드 조절 등의 게임 설정을 변경할 수 있습니다.|  

<br /><br />

|퍼즐|
|:---:|
|<img src="https://github.com/Dalsi-0/LEGENOofOuch/blob/main/ReadMe/Character.png?raw=true" width="700"/>|
|플레이어가 사용할 캐릭터를 선택할 수 있는 화면입니다.|

<br /><br />

|인벤토리|
|:---:|
|<img src="https://github.com/Dalsi-0/LEGENOofOuch/blob/main/ReadMe/play.png?raw=true" width="700"/>|
|실제 게임이 진행되는 화면으로 캐릭터가 이동하고 공격하며 적과 싸우는 장면입니다.|

<br /><br />

|스트레스|
|:---:|
|<img src="https://github.com/Dalsi-0/LEGENOofOuch/blob/main/ReadMe/play.png?raw=true" width="700"/>|
|실제 게임이 진행되는 화면으로 캐릭터가 이동하고 공격하며 적과 싸우는 장면입니다.|

<br /><br />

|로딩화면|
|:---:|
|<img src="https://github.com/Dalsi-0/LEGENOofOuch/blob/main/ReadMe/play.png?raw=true" width="700"/>|
|실제 게임이 진행되는 화면으로 캐릭터가 이동하고 공격하며 적과 싸우는 장면입니다.|

---

## 👥 팀원 및 역할

| 팀원 | 역할 | GitHub 링크 |
|---|---|---|
| **유재혁 (팀장)** | 역할 | [링크](https://github.com/jj930220s?tab=repositories)  |
| **김준혁** | 역할 | [링크](https://github.com/chajungto) |
| **정창범** | 역할 | [링크](https://github.com/JeongChangBeom) |
| **최상준** | 역할 | [링크](https://github.com/Dalsi-0) |
| **김다샘** | 역할 | [링크](https://github.com/DasaemKim) |

---

## 🤝 협업 툴

**GitHub:** 코드 버전 관리 및 협업

**Notion:** 프로젝트 문서 정리 및 일정 관리

**Figma:** 구조 설계 및 프로토타이핑

**Google Sheets:** 게임 데이터 관리

---

## 🛠 **개발 및 기술적 접근**

### 🔧 사용 기술 스택

**개발 엔진:** Unity  
**프로그래밍 언어:** C#  
**버전 관리:** GitHub  
**데이터 관리:** Google Sheets  
**라이브러리:** DOTween, UniRx  

### 📊 데이터 연동 (Google Spreadsheet)
이 프로젝트에서는 Google Spreadsheet를 이용하여 아이템 정보, 던전 데이터 등을 관리하고, 이를 코드에서 불러와 활용합니다.

🔗 **사용된 스프레드시트 데이터**
- 아이템 데이터 [(보기)](https://docs.google.com/spreadsheets/d/1It4-oR-oFmeYBxu8bO_hokMhEJwK96By3014bM6gt5c/edit?gid=0#gid=0)
- 세이브 파일 경로 [(보기)](https://docs.google.com/spreadsheets/d/1It4-oR-oFmeYBxu8bO_hokMhEJwK96By3014bM6gt5c/edit?gid=112729730#gid=112729730)
- 던전 정보 [(보기)](https://docs.google.com/spreadsheets/d/1It4-oR-oFmeYBxu8bO_hokMhEJwK96By3014bM6gt5c/edit?gid=1233562025#gid=1233562025)
  
🛠️ **데이터 로딩 방식**
```
// Google Spreadsheet에서 TSV 데이터 가져오기
const string URL_itemsSheet = "https://docs.google.com/spreadsheets/.../export?format=tsv&range=A1:F14";
const string URL_dungeonSheet = "https://docs.google.com/spreadsheets/.../export?format=tsv&gid=1233562025&range=A1:C4";
const string URL_savefilePathSheet = "https://docs.google.com/spreadsheets/.../export?format=tsv&gid=112729730&range=A1:D2";

using (HttpClient client = new HttpClient())
{
    var itemData = await client.GetStringAsync(URL_itemsSheet);
    var dungeonData = await client.GetStringAsync(URL_dungeonSheet);
    var savefilePathData = await client.GetStringAsync(URL_savefilePathSheet);
    
    // 비동기 작업이 완료될 때까지 기다립니다.
    await Task.WhenAll(itemData, dungeonData, savefilePathData);
    ...
}

---
## 🤔 기술적 이슈와 해결 과정 

### **유재혁**    
**📍 원인 분석**  
- ㅇ
- ㅇ

**💡 해결 방법**  
- ㅇ
- ㅇ 

<br />  

### **김준혁**  
**📍 원인 분석**  
- ㅇ
- ㅇ

**💡 해결 방법**  
- ㅇ 
- ㅇ 

<br />  

### **정창범**  
**📍 원인 분석**  
- ㅇ

**💡 해결 방법**  
- ㅇ 

<br />  

### **최상준**  
**📍 원인 분석**  
- ㅇ
- ㅇ

**💡 해결 방법**  
- ㅇ
- ㅇ

<br />  

### **김다샘**  
**📍 원인 분석**  
- ㅇ

**💡 해결 방법**  
- ㅇ 
- ㅇ

---

## 📹 플레이 영상

**[![유튜브](https://github.com/Dalsi-0/LEGENOofOuch/blob/main/ReadMe/main.png?raw=true)](https://youtu.be/0VoYswxdsbM)** 

---

## 🕹️ 플레이 링크  
**👉 [플레이하기](https://play.unity.com/ko/games/ea0ef296-8fc8-443e-8812-5eb56b48b2d9/legenoofouch)**

위 링크로 빌드파일을 다운받아서 바로 플레이할 수 있습니다! 🎮   

---


