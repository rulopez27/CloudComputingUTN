pipeline {
    agent any

    environment {
        DOTNET_CLI_TELEMETRY_OPTOUT = '1'
        DOTNET_SKIP_FIRST_TIME_EXPERIENCE = '1'
    }

    stages {
        stage('Restore') {
            steps {
                sh 'dotnet restore CloudComputingUTN.sln'
            }
        }
        stage('Build') {
            steps {
                sh 'dotnet build CloudComputingUTN.sln --configuration Release --no-restore'
            }
        }
        stage('Test and Coverage') {
            steps {
                sh 'dotnet test --no-build --verbosity normal --logger:"trx;LogFileName=test-results.trx"'
            }
        }
        stage('Convert TRX to JUnit') {
            steps {
                sh 'dotnet tool install -g trx2junit'
                sh 'export PATH="$PATH:$HOME/.dotnet/tools" && find . -name "*.trx" -exec trx2junit {} \\;'
            }
        }
    }
    post {
        always {
            junit allowEmptyResults: true, testResults: '**/TestResults/*.xml'
        }
    }
}