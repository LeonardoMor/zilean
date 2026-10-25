# Fork notes

This fork starts from [SolidRhino/zilean](https://github.com/SolidRhino/zilean) `main` at `61bfa90`. That is [Thoroslives/zilean](https://github.com/Thoroslives/zilean) plus SolidRhino's own work, and Thoroslives in turn forked the original [iPromKnight/zilean](https://github.com/iPromKnight/zilean). The history of all three is kept as it is, with the same commit hashes.

## Taken from other forks

- [Thoroslives/zilean PR #26](https://github.com/Thoroslives/zilean/pull/26) by yowmamasita ([fork](https://github.com/yowmamasita/zilean)), merged with its two original commits. DMM now publishes some hashlists as `#id=<UUID>` stubs with the payload in the hashlists repo under `lists/<UUID>.txt`. The scraper reads that file. A page that fails to decode is no longer saved as an empty parsed page, so it is retried on the next sync, and the migration `RetryFailedDmmHashlists` clears the zero-entry pages saved before the fix. The merge keeps the batched ParsedPages writes of the base as well.
- The IMDb `originalTitle` column from [cloudpftc/zilean](https://github.com/cloudpftc/zilean) (`bd5c13f` and `2a7158f`), adapted by hand: `ImdbFiles.OriginalTitle`, filled from the `originalTitle` column of the IMDb basics file. Existing rows are filled on the next IMDb import.

The API key log redaction (the key is logged at startup only as its first six characters) is SolidRhino's, from their [PR #3](https://github.com/SolidRhino/zilean/pull/3).

## Changes of this fork

- Season and episode markers (`S04`, `S04E05`, `4x05`, `Season 4`) are moved from the query text into the season and episode filters, unless the request already sets them. Before, they lowered the trigram similarity enough that season searches returned nothing.
- `t=tvsearch` is lowercased before it is mapped, so it selects the TV category.
- The `RetryFailedDmmHashlists` designer is aligned with the GiST trigram index, and the PR #26 tests are adapted to this base (`TorrentParser`, the `DmmService` factory constructor, a DbContext factory in the hashlist fixture).
- The image workflow builds and pushes `ghcr.io/h1n054ur/zilean` only when a `v*` tag is pushed. The automatic patch tag and release on every push to `main` were removed.

## Not taken from cloudpftc

- The migration adding four "missing" GIN indexes on `Torrents`. All four already exist here (`CleanedParsedTitleIndexes`, and the trigram index was moved to GiST by `SearchV8KnnSearch`). That migration would rebuild the trigram index as GIN and break the KNN search.
- Prowlarr sync and backfill, query cache, diagnostics, miss tracking, anime boosts, file logging, and its own fetch of stub payloads from the DMM list host. PR #26 covers the same case from the cloned repo.

## Other notes

- The DMM git clone and pull already retry with exponential backoff (5 attempts) in the base, so nothing was ported from [AyushSehrawat/zilean](https://github.com/AyushSehrawat/zilean).

## How this repository is kept

The GitHub repository gets the public branch by a manual push from the maintainer, as `main`, along with release tags. There is no mirror, so nothing arrives here on its own.

## Licence

MIT, see [LICENSE](LICENSE). The original author and the authors of each fork included here agreed to the MIT licence for their code.
