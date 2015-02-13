namespace Api

module UseCases =

    module Registration =
        
        open Representations

        let start (request : RegisterRepresentation) =
            Database.persist request