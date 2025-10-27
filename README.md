## StudentPortal – ASP.NET Core MVC CRUD 웹앱

## 프로젝트 개요

StudentPortal은 ASP.NET Core MVC를 사용해 구현한 학생 정보 관리용 웹 애플리케이션입니다.

이 프로젝트는 C# .NET 백엔드 실무 구조 (Controller / Service / Repository / DTO) 를 완벽히 이해하고,
Entity Framework Core를 활용한 데이터베이스 CRUD 처리를 학습하기 위해 제작되었습니다.

실무 구조 기반 + 취업 포트폴리오 제출용 코드 품질을 목표로 구성되었습니다.


## 주요 기능

- 학생 목록 조회 (Index)	DB에 저장된 학생 정보를 테이블 형태로 조회
- 학생 추가 (Create)	이름/나이 입력 후 새 학생 추가
- 학생 수정 (Edit)	기존 학생 정보를 수정 후 DB 반영
- 학생 삭제 (Delete)	삭제 버튼 클릭 시 해당 학생 정보 삭제
- 유효성 검사 (Validation)	이름/나이 입력 시 FluentValidation 검증
- DI(의존성 주입)	Service, Repository 자동 주입
- AutoMapper 적용	Entity ↔ DTO 변환 자동화
- MVC + REST API 병행 구조	MVC 화면용 컨트롤러와 API 컨트롤러를

## 기술 스택

- 언어	       C# (.NET 8.0)
- 프레임워크  	 ASP.NET Core MVC
- ORM	         Entity Framework Core (SQLite)
- DB	         SQLite
- DI	         내장 서비스 컨테이너 (AddScoped)
- AutoMapper	 객체 매핑용 라이브러리
- 디자인 도구   Figma (UI 와이어프레임 설계) 링크: https://www.figma.com/design/2usgeIN0zIgTWKAa6w7gGk/StudentPortalDesign?node-id=0-1&t=GTDPOCQnTBQDGtAB-1
- 프론트엔드    Razor View + HTML5 + CSS Grid
- 툴	Visual   Studio 2022 / GitHub

## 설계 포인트

- 설계 포인트
- Repository + Service 구조로 Controller 로직을 단순화
- DTO (Contracts) 로 View/Entity 간 의존성 분리
- 비동기(Async/Await) 기반으로 확장성 고려
- AutoMapper로 Entity ↔ Response 매핑 자동 처리
- MVC / API 분리 설계
→ 현재는 MVC만 사용하지만,
향후 React/Vue 프론트엔드 연동 시 API Controller 활용 가능
