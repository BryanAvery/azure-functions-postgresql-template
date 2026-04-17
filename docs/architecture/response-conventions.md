# Response Conventions

## Success responses

- For standard read/write operations, return `SuccessResponse<T>`.
- Include:
  - `data`
  - `correlationId`
  - `timestampUtc`

## Error responses

- Return `ProblemResponse` for any non-success response.
- Include `type`, `title`, `status`, `detail`, and `instance`.
- Validation failures should include an `errors` collection.

## Pagination responses

List endpoints should return:

- `items`
- `pageNumber`
- `pageSize`
- `totalCount`
- `hasNextPage`
