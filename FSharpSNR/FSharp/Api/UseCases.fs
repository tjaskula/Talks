namespace Api

module UseCases =

    module Registration =

        let start =
            Validation.account
            >> bind RegistrationService.shouldConfirmEmail
            //>> bind Database.findByEmailRegistration
            >> bind Database.persistRegistration