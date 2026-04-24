---
description: "Analyze the codebase for anything likely to break build or runtime execution (errors, misconfig, missing deps, fragile flows)"
name: "Analyze Runtime Risks"
argument-hint: "Optional scope (file/folder/feature), assumptions, and environment details"
agent: "agent"
---
Perform a failure-risk analysis of this repository with a focus on anything that could cause the project to fail when running.

If the user gives an argument, prioritize that scope first, then check related code paths.

## What to check
1. Build/compile blockers:
- Syntax/type errors
- Missing references/packages/usings/imports
- Invalid project configuration and incompatible target/framework settings

2. Startup/configuration risks:
- Missing required environment variables or settings
- Invalid or missing app configuration keys
- Misconfigured DI/service registration
- Incorrect middleware or app startup ordering

3. Runtime and logic hazards:
- Null/undefined reference risks
- Unhandled exceptions and brittle error handling
- Async/concurrency misuse
- File/path/permission assumptions that may fail in real environments

4. Integration/dependency risks:
- External service assumptions (DB, APIs, storage, auth)
- Version mismatch risks
- Platform-specific behavior that may fail cross-environment

5. Test and validation gaps:
- Missing tests for high-risk paths
- Areas where regressions are likely due to low coverage

## How to work
- Use repository search and focused file reads first.
- Execute available build/lint/test checks when useful.
- Avoid speculative claims: tie each finding to concrete evidence.

## Output format
Return results in this exact structure:

### Summary
- Short statement of overall run-risk level: High / Medium / Low
- Count findings by severity: Critical, High, Medium, Low

### Findings (ordered by severity)
For each finding, include:
- Severity:
- Location:
- Why it can fail at runtime/build time:
- Evidence:
- Recommended fix:
- Confidence: High / Medium / Low

### Quick Wins
- List the fastest 3 to 5 changes that would reduce the most risk.

### Validation Steps
- Provide exact commands/checks to confirm the fixes.

If no issues are found, explicitly say so and list residual risks and unverified assumptions.
