---
name: Documentation Validation
description: Validates that documentation is up to date with code changes in pull requests

on:
  pull_request: {}
  workflow_dispatch: {}

permissions:
  contents: read
  issues: read
  pull-requests: read
  copilot-requests: write

tools:
  github:
    mode: gh-proxy
    toolsets: [default]
  cli-proxy: true

safe-outputs:
  add-comment:
  # close-pull-request:
  # concurrency-group:
  # create-agent-session:
  # create-agent-task:
  # create-check-run:
  # create-code-scanning-alert:
  # create-discussion:
  # create-project:
  # create-project-status-update:
  # create-pull-request:
  # create-pull-request-review-comment:
  # dismiss-pull-request-review:
  # dismiss-review:
  # dispatch-repository:
  # dispatch-workflow:
  # dispatch_repository:
  # environment:
  # failure-issue-repo:
  # footer:
  # group-reports:
  # hide-comment:
  # id-token:
  # link-sub-issue:
  # mark-pull-request-as-ready-for-review:
  # max-bot-mentions:
  # max-patch-files:
  # mentions:
  # merge-pull-request:
  # missing-data:
---

# Documentation Validation

## Task

Review the pull request to validate that documentation is up to date with code changes.

Analyze:
1. Code changes: Identify modified source files and their purpose
2. Documentation files: Check if corresponding documentation has been updated
3. README and guides: Verify main documentation reflects new features or changes
4. Configuration changes: Ensure setup instructions are current

## Validation Rules

- If code introduces new public APIs, classes, or features: documentation should explain them
- If code changes existing behavior: documentation should be updated to match
- If configuration or setup changes: installation docs should reflect changes
- Minor refactoring or internal changes typically do not require doc updates

## Safe Outputs

When documentation issues are found:
Add a comment to the pull request using add-comment listing which code changes need documentation, suggested files to update, and specific sections to address.

When documentation is adequate:
Use noop with explanation: Documentation is up to date with the code changes in this pull request.

## Notes

- Run `gh aw compile` to generate the GitHub Actions workflow
- See https://github.github.com/gh-aw/ for complete configuration options and tools documentation
