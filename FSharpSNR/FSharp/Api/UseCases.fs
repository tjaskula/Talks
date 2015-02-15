namespace Api

module UseCases =

    module Registration =

        let start =
            Validation.account
            >> bind Database.persistRegistration