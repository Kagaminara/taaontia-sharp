# REFACTORS

Currently, clients of TaaontiaCore receive instanciated DB models which gives them access to waaaaay too many DB-related things.
A future refactor should include DTOs to encapsulate the core from the rest