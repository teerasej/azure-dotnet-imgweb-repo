---
name: meeting-preparation
description: "Prepare meeting materials in the workspace and generate a PowerPoint locally by default, or with WorkIQ when explicitly requested. Use when: prepare meeting, create meeting prep, meeting briefing, presenter notes, summarize recent changes, generate meeting diagrams, create meeting deck, create meeting folder"
---

# Meeting Preparation

Prepare a complete meeting package in the workspace and generate the PowerPoint locally by default.

## Goal

For each meeting request, create a folder at `meeting/yyyy-mm-dd-xx` and store the working artifacts there:

- `presenter.md` with a concise summary of recent changes and presenter guidance
- One or more diagram assets generated as draw.io-compatible files and exported as PNG or SVG when possible
- Any supporting files needed for the presentation workflow

Then create the PowerPoint locally unless the user explicitly asks to use WorkIQ.

## Folder Rules

1. Use the meeting date in `yyyy-mm-dd` format.
2. Use `xx` as a short lowercase slug derived from the meeting topic, for example `release-review` or `weekly-sync`.
3. Create the folder before generating artifacts.
4. Keep all generated files inside that meeting folder, preferably under an `assets/` subfolder for images and diagram sources.

## Required Inputs

Gather only what is missing:

- Meeting title or topic
- Meeting date if it should not use the current date
- Audience or meeting type if it affects the summary tone
- Scope of recent changes to summarize, such as branch, feature area, timeframe, notes file, or folder
- Whether the user explicitly wants WorkIQ to create the presentation instead of the default local workflow

If the scope of recent changes is unclear, ask a concise question before generating files.

## Presentation Backend Rules

1. Default to local presentation creation inside the meeting folder.
2. Install any required package or library for local presentation generation when needed.
3. Use WorkIQ to create the PowerPoint file only when the user explicitly asks for WorkIQ.
4. If WorkIQ is used, still prepare and keep the local meeting artifacts first.

## Workflow

1. Create the meeting folder at `meeting/yyyy-mm-dd-xx`.

2. Gather recent changes from the most relevant local sources, such as:
   - Current workspace files
   - Git changes or recent history when available
   - Notes already present in the repository
   - User-provided context

3. Write `presenter.md` in the meeting folder. It should usually include:
   - Meeting title and date
   - Audience or purpose
   - Executive summary of recent changes
   - Key talking points
   - Risks, open questions, or decisions needed
   - Embedded diagram image references using relative Markdown paths

4. Generate the necessary diagram assets:
   - Create draw.io-compatible source files, for example under `assets/architecture.drawio`
   - Export matching PNG or SVG files, for example `assets/architecture.png` or `assets/architecture.svg`
   - Embed the exported image files in `presenter.md`
   - When creating presentation slides, include the PNG or SVG version in the relevant slide when available and add a short explanation of what the diagram shows

5. If PNG or SVG export is blocked because draw.io export tooling is unavailable, still create the draw.io source, explain the blocker clearly, and continue saving the rest of the meeting package.

6. Generate the PowerPoint locally by default and save it inside the meeting folder. If local generation requires a presentation library, install it before creating the file.

7. If the user explicitly asks to use WorkIQ, generate the PowerPoint through WorkIQ. Use a natural-language request that includes:
   - The meeting title and audience
   - The summarized recent changes from `presenter.md`
   - The intended slide flow
   - References to exported PNG or SVG diagram assets when they exist, including a request to explain the diagram in the relevant slide
   - A request for the direct file link to the created presentation

8. If WorkIQ does not return a file URL, ask again explicitly for the direct presentation link before finishing.

## Presenter Markdown Guidance

Keep `presenter.md` concise and presentation-ready. Prefer short sections, clear bullets, and references to the generated diagrams. The markdown should help a presenter speak to the changes without reading raw commit history.

## Presentation Guidance

When creating slides locally or through WorkIQ:

- Prefer using exported PNG or SVG versions of draw.io diagrams instead of raw `.drawio` files in slides
- Add a short explanation near each diagram slide so the presenter can explain why the diagram matters
- If no exported image is available, mention the missing export as a blocker rather than pretending the slide asset exists

## Output Format

When the workflow is complete, present:

- The meeting folder path
- A short summary of the created artifacts
- The local presentation file path, or if WorkIQ was explicitly used, the direct presentation link labeled Open Presentation
- Any blockers, especially if PNG or SVG export could not be completed
