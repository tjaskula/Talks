namespace Api

module UseCases =

    module Registration =

        let start =
            Database.persistRegistration